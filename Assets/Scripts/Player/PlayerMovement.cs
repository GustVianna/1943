using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private int _speed = 300;

        private Rigidbody2D _mRb;
        private float _movX, _movY;

        private Vector2 _screenLimits; //Os limites da tela para o player não sair dela
        private float _objectWidth, _objectHeight; //Para o jogador não parar no meio da sprite e sim nas bordas isso é necessário

        private Animator _animator;

        private Animator _secondSpriteAnimator; //Sprite danificada do aviao

        // Start is called before the first frame update
        void Start()
        {
            _mRb = GetComponent<Rigidbody2D>();
            _screenLimits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
            _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y/2;

            _animator = GetComponent<Animator>();
            _secondSpriteAnimator = GameObject.Find("SubSprite").GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            _movX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
            _movY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        }

        private void FixedUpdate()
        {

            MoveOnX();
            MoveOnY();

            
            _animator.SetFloat("SpeedX", _mRb.velocity.x); //animações

            _secondSpriteAnimator.SetFloat("SpeedX2", _mRb.velocity.x); //A segunda sprite é animada junta

            MovementLimit();
        }

        void MoveOnX()
        {
            Vector2 velocityX = _mRb.velocity;
            velocityX.x = _movX;
            _mRb.velocity = velocityX;
        }

        void MoveOnY()
        {
            Vector2 velocityY = _mRb.velocity;
            velocityY.y = _movY;
            _mRb.velocity = velocityY;
        }

        void MovementLimit()
        {
            Vector3 _pos = transform.position;
            _pos.x = Mathf.Clamp(_pos.x, _screenLimits.x * -1 + _objectWidth, _screenLimits.x - _objectWidth);
            _pos.y = Mathf.Clamp(_pos.y, _screenLimits.y * -1 + _objectHeight, _screenLimits.y - _objectHeight);
            transform.position = _pos;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class Boss : MonoBehaviour
    {
        private Vector2 _screenLimits; //Os limites da tela para o player não sair dela

        private float _randomX;
        private float _randomY;
        private float _timeChange;


        private bool _canMove; //Se o boss ja pode se mexer

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private GameObject[] _turrets;

        // Start is called before the first frame update
        void Start()
        {
            _screenLimits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void OnBecameVisible()
        {
            _canMove = true;
        }

        private void FixedUpdate()
        {
            Movement();

            
        }

        void MovementLimit()
        {
            Vector3 _pos = transform.position;
            _pos.x = Mathf.Clamp(_pos.x, _screenLimits.x * -1, _screenLimits.x);
            _pos.y = Mathf.Clamp(_pos.y, _screenLimits.y * -1, _screenLimits.y);
            transform.position = _pos;
        }

        void Movement()
        {
            if (_canMove)
            {
                if (Time.time >= _timeChange)
                {
                    _randomX = Random.Range(-10, 10); //Distancias random
                    _randomY = Random.Range(-10, 10);

                    _timeChange = Time.time + Random.Range(2, 4); //Delay aleatorio entre um movimento e outro
                    moveSpeed = Random.Range(0.1f, 0.3f); //Velocidade random
                }
                transform.Translate(new Vector3(_randomX, _randomY, 0) * moveSpeed * Time.deltaTime);
                // if object reached any border, revert the appropriate direction
                if (transform.position.x >= _screenLimits.x || transform.position.x <= _screenLimits.x * -1)
                {
                    _randomX = -_randomX;
                }
                if (transform.position.y >= _screenLimits.y || transform.position.y <= _screenLimits.y * -1)
                {
                    _randomY = -_randomY;
                }

                MovementLimit();
            }

            else return;
        }
    }
}
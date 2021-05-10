using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gustavo.GameMechanics
{
    public class Energy : MonoBehaviour
    {
        //O player não tem vida nesse jogo, mas sim essa energia que perde com o tempo
        public float energyValue = 40;

        //Texto de energia
        public Text energyText;

        //Para o gameover
        private SceneController _controller;

        private bool _canTakeDamage; //Para a animação de tomar dano
        private int _invincibilityTime;
        private bool _canPlay; ///para a coroutine n ficar rodando

        //Para desligar quando o jogador morrer
        private SpriteRenderer _sprite;

        private SpriteRenderer _secondSprite; //A sprite danificada do aviao

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.enabled = true;
            _controller = FindObjectOfType<SceneController>();
            energyValue = 40;

            _canPlay = true;
            _canTakeDamage = true;
            _invincibilityTime = 3;

            _secondSprite = GameObject.Find("SubSprite").GetComponent<SpriteRenderer>();
            _secondSprite.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            energyValue -= Time.deltaTime;

            if (energyValue < 0)
                energyValue = 0;

            int temp = (int)energyValue;
            energyText.text = temp.ToString("000");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && _canTakeDamage|| collision.gameObject.CompareTag("Enemy Bullet") && _canTakeDamage)
            {
                ChangeEnergy(-10);
                if(_canPlay)
                StartCoroutine(Invincibility());
            }
        }

        //Quando o objeto toma dano
        public void ChangeEnergy(int newEnergyValue)
        {
            if (energyValue > 0) //Caso for maior que zero ele toma dano, caso o contrario é game over
                energyValue += newEnergyValue; // O player perde 10 de energia quando toma dano

            else
            {
                _controller.LoseCondition(); //Para o game over
            }
        }

        void IsDamaged() //Ficar um tempo invencivel apos tomar dano
        {

        }

        IEnumerator Invincibility()
        {
            _canPlay = false;

            _secondSprite.enabled = true;  //Animation damage
            _canTakeDamage = false;
            yield return new WaitForSeconds(_invincibilityTime); //Tempo de invencibilidade
            _canTakeDamage = true;
            _secondSprite.enabled = false;

            _canPlay = true;
        }
    }
}

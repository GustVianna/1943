using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gustavo.ShootOptions;
using Gustavo.GameMechanics;


namespace Gustavo.CharactersOptions
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private bool _isTurret; //Como a vida das torretas está ligada ao fim do jogo isso é necessário
        static int _turretInstance; //Contagem de turrets para o boss morrer

        //O valor da vida inicial do objeto
        public int healthValue;

        private int _contactDamage = 3; //Dano de contato com o jogador

        private Bullet _bulletScript; //Necessário para ver quanto dano a bala dá

        private int _chanceToDrop; //Chance de dropar powerup

        private SceneController controller;
        /// 

        private void Awake()
        {
            _contactDamage = 3;

            _turretInstance = 9;

            if (gameObject.CompareTag("Turret"))
            {
                _isTurret = true;
            }

            controller = FindObjectOfType<SceneController>();
        }

        //Essa função serve para adicionar ou reduzir a vida através de somas e subtrações.
        public void ChangeHealth(int newHealthValue)
        {
            healthValue += newHealthValue;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                TookDamage(-_contactDamage); //Tocar no player faz com que tomem 1 de dano

            else if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("BigBullet") || collision.gameObject.CompareTag("CircularBullet") || collision.gameObject.CompareTag("SmallBullet"))
            {
                _bulletScript = collision.gameObject.GetComponent<Bullet>();
                TookDamage(-_bulletScript.bulletDamage); //O dano da bala depende de seu upgrade, entao é variavel
            }
        }

        void TookDamage(int _damageAmount)
        {
            ChangeHealth(_damageAmount);

            if (healthValue <= 0 && !_isTurret)
                EnemyDeath();

            else if (healthValue <= 0 && _isTurret)
            {
                if (_turretInstance > 1) //Ultima torreta
                {
                    EnemyDeath();
                    _turretInstance--;
                }

                else
                {
                    //Grande explosao do boss
                    GameObject explosion = Pooling.SharedInstance.GetPooledObject("Explosion");
                    if (explosion != null)
                    {
                        explosion.transform.position = transform.position;
                        explosion.transform.rotation = transform.rotation;
                        explosion.transform.localScale = transform.localScale * 10;
                        explosion.SetActive(true);
                    }

                    EnemyDeath();
                    _turretInstance--;
                    controller.WinCondition();
                }

            }
        }

        void EnemyDeath()
        {
            _chanceToDrop = Random.Range(0, 100); //Dropa power up com 10%
            if (_chanceToDrop <= 10) //10%
            {
                SpawnPowerUp(transform.position);
            }
            

            //Instancia eplosao
            GameObject explosion = Pooling.SharedInstance.GetPooledObject("Explosion");
            if (explosion != null)
            {
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
                explosion.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        void SpawnPowerUp(Vector3 pos) //Os inimigos tem chance de spawnar powerups
        {
            GameObject powerup = Pooling.SharedInstance.GetPooledObject("PowerUp");
            if (powerup != null)
            {
                powerup.transform.position = transform.position;
                powerup.transform.rotation = Quaternion.identity;
                powerup.SetActive(true);
            }
        }

        private void Update()
        {
            print(_turretInstance);
        }

        private void OnDestroy() //Como as torretas estao ligadas com o fim do jogo, elas não precisam de pooling e facilita a contagem dessa maneira
        {
            _turretInstance--;
        }
    }
}

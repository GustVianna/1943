using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gustavo.ShootOptions;
using Gustavo.GameMechanics;


namespace Gustavo.CharactersOptions
{
    public class EnemyHealth : MonoBehaviour
    {
        //O valor da vida inicial do objeto
        public int healthValue;

        private int _contactDamage = 1; //Dano de contato com o jogador

        private Bullet _bulletScript; //Necessário para ver quanto dano a bala dá

        private int _chanceToDrop; //Chance de dropar powerup

        //Essa função serve para adicionar ou reduzir a vida através de somas e subtrações.
        public void ChangeHealth(int newHealthValue)
        {
            healthValue += newHealthValue;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                TookDamage(-_contactDamage); //Tocar no player faz com que tomem 1 de dano

            else if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("BigBullet") || collision.gameObject.CompareTag("CircularBullet"))
            {
                _bulletScript = collision.gameObject.GetComponent<Bullet>();
                TookDamage(-_bulletScript.bulletDamage); //O dano da bala depende de seu upgrade, entao é variavel
            }
        }

        void TookDamage(int _damageAmount)
        {
            ChangeHealth(_damageAmount);

            if (healthValue <= 0)
                StartCoroutine(EnemyDeath());
        }

        IEnumerator EnemyDeath()
        {
            _chanceToDrop = Random.Range(0, 100); //Dropa power up com 10%
            if (_chanceToDrop <= 10) //10%
            {
                SpawnPowerUp(transform.position);
            }

            //Instancia eplosao
            yield return new WaitForSeconds(0.5f); //Um pequeno delay para a explosão acontecer
            gameObject.SetActive(false);
        }

        void SpawnPowerUp(Vector3 pos) //Os inimigos tem chance de spawnar powerups
        {
            GameObject powerup = Pooling.SharedInstance.GetPooledObject("PowerUp");
            if (powerup != null)
            {
                powerup.transform.position = transform.position;
                powerup.transform.rotation = transform.rotation;
                powerup.SetActive(true);
            }
        }
    }
}

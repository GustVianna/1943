using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.ShootOptions
{
    public class Bullet : MonoBehaviour
    {
        public int bulletDamage; // Quanto dano a bala dá
        public string bulletType; 

        private Rigidbody2D _mRb;
        

        private void Awake()
        {
            _mRb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PowerUp"))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnBecameInvisible()
        {
            //Quando o tiro sai da tela
            gameObject.SetActive(false);

        }

        private void OnEnable()
        {

            //Quando é usado de novo, dispara
            switch (bulletType)
            {
                case "Charge":
                    ShootBullet(40);
                    break;

                case "Enemy":
                    ShootBullet(5);
                    break;

                case "Default":
                    ShootBullet(20);
                    break;

                case "Circle":
                    ShootBullet(10);
                    StartCoroutine(BulletDie(0.4f));
                    break;
            }
        }

        void ShootBullet(int bulletForce)
        {
            _mRb.AddRelativeForce(Vector3.up * bulletForce, ForceMode2D.Impulse);
        }

        IEnumerator BulletDie(float seconds) //Para um tipo de bala morrer antes de sair da tela
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}
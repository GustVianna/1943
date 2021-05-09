using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gustavo.GameMechanics;


namespace Gustavo.ShootOptions
{
    public class Shoot : MonoBehaviour
    {
        public int powerupType; //O tipo do tiro (basico, power up 1, power up 2)

        private float _chargeRate = 1; //Quanto tempo precisa segurar o botão para o tiro forte
        private float _chargeCurrent = 0; //Quanto tempo já está segurando
        private bool _chargeOK = false; //Quantidade de charge suficiente ou não

        public float ammoCount = 40; //A munição funciona por tempo da mesma maneira que a energia do player

        // Update is called once per frame
        void Update()
        {

            //Atirar
            if (Input.GetKeyDown(KeyCode.J))
            {
                switch (powerupType)
                {
                    case 1:
                        Fire1();
                        break;

                    case 2:
                        Fire2();
                        break;

                    case 3:
                        Fire3();
                        break;
                }
            }

            //Segurar para o tiro forte
            if (Input.GetKey(KeyCode.J))
            {
                _chargeCurrent += Time.deltaTime;

                if (_chargeCurrent > _chargeRate)
                {
                    _chargeOK = true;
                }

                else
                    _chargeOK = false;
            }

            //Soltar tiro forte
            if (Input.GetKeyUp(KeyCode.J))
            {
                _chargeCurrent = 0;

                if (_chargeOK == true)
                {
                    FireBig();
                    Debug.Log("Fire!");
                }

                else
                    Debug.Log("Not enough charge");

            }

            //Munição decai com o tempo, e não com o numero de usos
            //ammoCount -= Time.deltaTime;
            //
            //if (ammoCount < 0)
            //    ammoCount = 0;
        }

        

        //Tiro forte
        void FireBig()
        {
            //Tiro forte
            GameObject bullet = Pooling.SharedInstance.GetPooledObject("BigBullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }

        void Fire1()
        {
            //Linha dupla comum
            GameObject bullet = Pooling.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }

        }

        //Tiro com powerup 1
        void Fire2()
        {
            //Circular
            GameObject bulletLL = Pooling.SharedInstance.GetPooledObject("CircularBullet");
            if (bulletLL != null)
            {
                bulletLL.transform.position = transform.position;
                bulletLL.transform.rotation = Quaternion.Euler(0, 0, -60);
                bulletLL.SetActive(true);
            }

            GameObject bulletL = Pooling.SharedInstance.GetPooledObject("CircularBullet");
            if (bulletL != null)
            {
                bulletL.transform.position = transform.position;
                bulletL.transform.rotation = Quaternion.Euler(0, 0,-30);
                bulletL.SetActive(true);
            }
            GameObject bullet = Pooling.SharedInstance.GetPooledObject("CircularBullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            GameObject bulletR = Pooling.SharedInstance.GetPooledObject("CircularBullet");
            if (bulletR != null)
            {
                bulletR.transform.position = transform.position;
                bulletR.transform.rotation = Quaternion.Euler(0, 0, 30);
                bulletR.SetActive(true);
            }

            GameObject bulletRR = Pooling.SharedInstance.GetPooledObject("CircularBullet");
            if (bulletRR != null)
            {
                bulletRR.transform.position = transform.position;
                bulletRR.transform.rotation = Quaternion.Euler(0, 0, 60);
                bulletRR.SetActive(true);
            }
        }

        

        //Tiro com powerup 2
        void Fire3()
        {
            //reto e 2 diagonais
            GameObject bullet = Pooling.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }

            GameObject bulletLeft = Pooling.SharedInstance.GetPooledObject("SmallBullet"); ///Como as sprites e os danos dos diferentes tiros são diferentes, existem diferentes objetos de tiro
            if (bulletLeft != null)
            {
                bulletLeft.transform.position = transform.position;
                bulletLeft.transform.rotation = Quaternion.Euler(0,0, -40);
                bulletLeft.SetActive(true);
            }

            GameObject bulletRight = Pooling.SharedInstance.GetPooledObject("SmallBullet"); ///Como as sprites e os danos dos diferentes tiros são diferentes, existem diferentes objetos de tiro
            if (bulletRight != null)
            {
                bulletRight.transform.position = transform.position;
                bulletRight.transform.rotation = Quaternion.Euler(0, 0, 40);
                bulletRight.SetActive(true);
            }
        }
    }
}

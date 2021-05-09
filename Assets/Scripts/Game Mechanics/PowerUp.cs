using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gustavo.ShootOptions;

namespace Gustavo.GameMechanics
{
    public class PowerUp : MonoBehaviour
    {
        private string _powerUpType; //Tipo de power, segue a ordem powerup1 -> powerup2 -> energia
        [SerializeField]
        private GameObject[] powerUpObjects; //Os diferentes objetos que formam um powerup

        [SerializeField]
        private float _amountToGoUp = 0.5f; //Quanto o powerup sobe depois de tomar tiro

        private int _shotCount= 5; //Quantos tiros precisa pro upgrade mudar
        private bool _canUpgrade = true; //Para ver se o upgrade ainda consegue melhorar ou se já chegou no maximo

        private Energy _energyScript; //Para aumentar a energia
        private Shoot _shootScript; //Para mudar o tiro

        private void OnEnable()
        {
            //Reseta tudo
            ResetPowerUp();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _energyScript = collision.gameObject.GetComponent<Energy>();
                _shootScript = collision.gameObject.GetComponent<Shoot>();
                GivePowerUp();
                gameObject.SetActive(false);
                //some, da o power up
            }

            else if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("BigBullet") || collision.gameObject.CompareTag("CircularBullet"))
            {
                if (_canUpgrade)
                {
                    Vector3 pos = transform.position;
                    pos.y = transform.position.y + _amountToGoUp;
                    transform.position = pos;

                    _shotCount--; // Quando toma tiro perde a contagem até o upgrade melhorar
                    if (_shotCount <= 0)
                    {
                        UpgradePower();
                        
                    }
                }

                else return;
            }
        }

        void ResetPowerUp()
        {
            _powerUpType = "Powerup1";
            powerUpObjects[0].SetActive(true);
            powerUpObjects[1].SetActive(false);
            powerUpObjects[2].SetActive(false);
            _canUpgrade = true;
            _shotCount = 5;
        }

        void GivePowerUp()
        {
            switch (_powerUpType) //Mesmo sendo apenas 2 power ups, esse switch é necessário para scalabilidade
            {
                case "Energy":
                    _energyScript.ChangeEnergy(20);
                    //Restaura 20 de energia
                    break;

                case "Powerup1":
                    _shootScript.powerupType = 2;
                    break;

                case "Powerup2":
                    _shootScript.powerupType = 3;
                    break;
            }
        }

        void UpgradePower()
        {
            switch (_powerUpType)
            {
                case "Powerup1":
                    _powerUpType = "Powerup2";
                    powerUpObjects[0].SetActive(false);
                    powerUpObjects[1].SetActive(true);
                    _shotCount = 5;
                    break;

                case "Powerup2":
                    _powerUpType = "Energy";
                    powerUpObjects[1].SetActive(false);
                    powerUpObjects[2].SetActive(true);
                    _shotCount = 5;
                    _canUpgrade = false;
                    break;
            }
        }
    }
}
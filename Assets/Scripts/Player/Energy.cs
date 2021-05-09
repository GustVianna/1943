using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.GameMechanics
{
    public class Energy : MonoBehaviour
    {
        //O player não tem vida nesse jogo, mas sim essa energia que perde com o tempo
        public float energyValue = 40;

        // Update is called once per frame
        void Update()
        {
            energyValue -= Time.deltaTime;

            if (energyValue < 0)
                energyValue = 0;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy Bullet"))
                ChangeEnergy(-10);
        }

        //Quando o objeto toma dano
        public void ChangeEnergy(int newEnergyValue)
        {
            if (energyValue > 0) //Caso for maior que zero ele toma dano, caso o contrario é game over
                energyValue += newEnergyValue; // O player perde 10 de energia quando toma dano

            else
                GameOver();
        }

        void GameOver()
        {

        }
    }
}

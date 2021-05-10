using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.GameMechanics
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _typeOfEnemyToSpawn; //0 pequeno, 1 grande

        

        

        private int _chanceToSpawn;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        IEnumerator SpawnWaves()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                _chanceToSpawn = Random.Range(0, 100);
                if (_chanceToSpawn <= 80) //50%
                {
                    GameObject enemy = Pooling.SharedInstance.GetPooledObject("Enemy");
                    if (enemy != null)
                    {
                        enemy.transform.position = transform.position;
                        enemy.transform.rotation = transform.rotation;
                        enemy.SetActive(true);
                    }
                }

                yield return null;
            }
        }
        void SpawnEnemy()
        {
            
        }
    }
}
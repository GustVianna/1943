using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gustavo.GameMechanics;

namespace Gustavo.CharactersOptions
{
    public class EnemyFire : MonoBehaviour
    {
        private int _chanceToShoot; //A porcentagem de chance a cada update lento do inimigo atirar

        GameObject player;//Para atirar no player

        private void OnEnable()
        {
            player = GameObject.Find("Player");//Ver onde o player está
            StartCoroutine(SlowUpdate());
        }

        IEnumerator SlowUpdate() //Como os inimigos não dão tiros frequentemente, para não consumir muita memória é melhor fazer um update lento que atira as vezes
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                _chanceToShoot = Random.Range(0, 100);
                if (_chanceToShoot <= 40) //40%
                {
                    EnemyShoot();
                }

                yield return null;
            }
        }

        void EnemyShoot()
        {
            GameObject bullet = Pooling.SharedInstance.GetPooledObject("Enemy Bullet");
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                Vector2 lookDir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); ///No jogo original a IA não é "nerfada", ou seja, elas atiram exatamente onde o player está, o que facilita é que os inimigos estão sempre em movimento, e o player também
                bullet.SetActive(true);
            }
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class Turret : MonoBehaviour
    {
        GameObject player;//Para atirar no player

        private void OnEnable()
        {
            player = GameObject.Find("Player");//Ver onde o player está
        }

        private void Update()
        {
            RotateToPlayer();
        }

        void RotateToPlayer() //Canhão gira pro player
        {
            Vector2 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
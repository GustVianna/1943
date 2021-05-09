using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.GameMechanics
{
    public class DisappearOffScreen : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            //Quando o objeto sai da tela ele some
            gameObject.SetActive(false);
        }
    }
}
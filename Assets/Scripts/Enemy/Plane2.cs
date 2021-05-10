using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class Plane2 : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Translate(new Vector3 (speed,0));
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class Plane2 : MonoBehaviour
    {
        private Vector3 desiredPos;

        [SerializeField]
        private int _distance;

        private Vector3 center, direction;

        void Start()
        {
            Vector3 originalPos = transform.position;
            center = originalPos + ((desiredPos - originalPos) / 2f);
            direction = Vector3.up * _distance;
        }

        void Update()
        {
            transform.position = center + Mathf.Sin(Time.time) * direction;
        }
    }
}
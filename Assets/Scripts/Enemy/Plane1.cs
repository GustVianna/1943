using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.CharactersOptions
{
    public class Plane1 : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 1f;

        [SerializeField]
        private float _frequency = 5f;

        [SerializeField]
        private float _magnitude = 1f;

        private bool _facingRight = true;

        Vector3 pos;
        

        void OnEnable()
        {

            pos = transform.position;

        }

        // Update is called once per frame
        void Update()
        {
            SinMove();
        }

        void SinMove()
        {
            pos += transform.right * Time.deltaTime * _moveSpeed;
            transform.position = pos + transform.up * Mathf.Sin(Time.time * _frequency) * _magnitude;
        }
    }
}
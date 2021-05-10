using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.GameMechanics
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField]
        private bool _parallaxDown;

        [SerializeField]
        private float parallaxSpeed = 0.01f;

        [SerializeField]
        private bool _isPlaying = true; //Para determinar quando parar e seguir o parallax

        [SerializeField]
        private bool _stopsWhenInView = false;

        [SerializeField]
        private bool _isInfinite;


        public Transform[] movableObjects;
        public float height = 16; //Para parallax infinito
        // Update is called once per frame
        void Update()
        {
            if (_isPlaying)
            {
                if (_parallaxDown)
                    ParallaxDown();

                else
                    ParallaxUp();
            }

            else
                return;

            if (_isInfinite)
            {
                for (int i = 0; i < movableObjects.Length; i++)
                {

                    if (movableObjects[i].position.y <= height * -2)
                    {
                        movableObjects[i].Translate(new Vector2(0, height*2));
                    }
                }
            }
        }

        private void OnBecameVisible() //Quando aparece na view
        {
            if (_stopsWhenInView)
                StopParallax();
        }

        void StopParallax() //Parar o parallax por algum motivo, seja tempo, posição na tela, etc
        {
            _isPlaying = false;
        }

        void ParallaxDown()
        {
            gameObject.transform.Translate(0, -parallaxSpeed, 0);
        }

        void ParallaxUp()
        {
            gameObject.transform.Translate(0, +parallaxSpeed, 0);
        }
    }
}
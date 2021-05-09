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
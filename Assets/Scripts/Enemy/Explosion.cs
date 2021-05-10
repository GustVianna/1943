using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gustavo.ShootOptions
{
    public class Explosion : MonoBehaviour
    {
        void BecomeInactive()
        {
            gameObject.SetActive(false);
        }
    }
}
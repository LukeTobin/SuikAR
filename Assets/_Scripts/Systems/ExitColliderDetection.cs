using System;
using SuikAR.Fruits;
using UnityEngine;

namespace SuikAR.Systems
{
    public class ExitColliderDetection : MonoBehaviour
    {
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent(out FruitObject fruitObject))
            {
                fruitObject.gameObject.SetActive(false);
            }
        }
    }
}
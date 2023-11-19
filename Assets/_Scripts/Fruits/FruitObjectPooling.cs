using System.Collections;
using System.Collections.Generic;
using SuikAR.Fruits;
using UnityEngine;

namespace SuikAR.Fruits
{
    public class FruitObjectPooling : MonoBehaviour
    {
        [SerializeField] private GameObject fruitObjectPrefab;
        [SerializeField] private int amountToPool;

        private List<FruitObject> objectPool = new List<FruitObject>();
        private GameObject container;

        private void Awake()
        {
            FillPool();
        }

        /// <summary>
        /// Fill our new pool into a new empty object
        /// </summary>
        private void FillPool()
        {
            container = new GameObject("FruitObject Pool")
            {
                transform =
                {
                    parent = transform
                }
            };

            for (int i = 0; i < amountToPool; i++)
            {
                InstantiateFruitObject();
            }
        }

        public FruitObject Retrieve()
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].isActiveAndEnabled)
                {
                    return objectPool[i];
                }
            }

            return ExtendPool();
        }

        private FruitObject ExtendPool()
        {
            InstantiateFruitObject();
            return objectPool[^1];
        }

        private void InstantiateFruitObject()
        {
            FruitObject obj = Instantiate(fruitObjectPrefab, container.transform).GetComponent<FruitObject>();
            obj.gameObject.SetActive(false);
            objectPool.Add(obj);
        }
    }
}

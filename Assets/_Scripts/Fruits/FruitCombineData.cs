using System.Collections.Generic;
using UnityEngine;

namespace SuikAR.Fruits
{
    public class FruitCombineData
    {
        public Fruit currentFruit;
        public Vector3 combinePosition;
        
        public List<GameObject> fruitObjects = new List<GameObject>();

        public FruitCombineData(FruitObject a, FruitObject b, FruitObject c)
        {
            currentFruit = a.Fruit;
            
            fruitObjects.Add(a.gameObject);
            fruitObjects.Add(b.gameObject);
            fruitObjects.Add(c.gameObject);
            
            combinePosition = (a.gameObject.transform.position + b.gameObject.transform.position + c.gameObject.transform.position) / 3;
        }
    }
}
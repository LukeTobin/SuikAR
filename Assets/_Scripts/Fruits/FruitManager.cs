using SuikAR.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SuikAR.Fruits
{
    public class FruitManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FruitObjectPooling objectPooler;

        [SerializeField] private Fruit[] spawnableFruit;
        
        private Fruit nextFruit;

        private void Awake()
        {
            GetNextFruit();
        }

        public FruitObject GetCurrentFruit()
        {
            FruitObject obj = objectPooler.Retrieve();
            obj.Initialize(nextFruit, new Vector3());
            
            GetNextFruit();

            return obj;
        }

        private void GetNextFruit()
        {
            nextFruit = spawnableFruit[Random.Range(0, spawnableFruit.Length)];
            EventManager.Invoke(EventManager.Event.OnFruitQueued, nextFruit);
        }
        
        private void CombineFruit(FruitCombineData fruitData)
        {
            objectPooler.Retrieve().Initialize(fruitData.currentFruit.combineFruit, fruitData.combinePosition);
            foreach (GameObject obj in fruitData.fruitObjects)
            {
                obj.SetActive(false);
            }
        }

        private void OnEnable()
        {
            EventManager.Subscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, CombineFruit);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, CombineFruit);
        }
    }
}
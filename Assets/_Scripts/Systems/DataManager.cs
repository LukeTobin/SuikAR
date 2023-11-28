using System;
using SuikAR.Events;
using SuikAR.Fruits;
using UnityEngine;

namespace SuikAR.Systems
{
    public class DataManager : MonoBehaviour
    {
        private Score currentGameScore;

        private void UpdateGameScore(FruitCombineData data)
        {
            currentGameScore += data.currentFruit.combineScore;
        }

        private void OnEnable()
        {
            currentGameScore = new Score();
            EventManager.Subscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, UpdateGameScore);  
        }
        
        private void OnDisable()
        {
            EventManager.Unsubscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, UpdateGameScore);
        }
    }
}
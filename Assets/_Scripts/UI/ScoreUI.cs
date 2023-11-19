using SuikAR.Events;
using SuikAR.Fruits;
using UnityEngine;
using TMPro;

namespace SuikAR.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentScoreText;
        [SerializeField] private TMP_Text highScoreText;

        private int Score
        {
            get => score;
            set
            {
                score = value;
                currentScoreText.text = value.ToString();

                if (score > highscore)
                {
                    highscore = score;
                    highScoreText.text = score.ToString();
                }
            }
        }

        private int score = 0;
        private int highscore = 0;

        private void AddScore(FruitCombineData fruitData)
        {
            Score += fruitData.currentFruit.combineScore;
        }
        
        private void OnEnable()
        {
            EventManager.Subscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, AddScore);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<FruitCombineData>(EventManager.Event.OnFruitCombine, AddScore);
        }
    }
}
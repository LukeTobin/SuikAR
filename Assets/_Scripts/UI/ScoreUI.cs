using SuikAR.Events;
using SuikAR.Fruits;
using SuikAR.Systems;
using UnityEngine;
using TMPro;

namespace SuikAR.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentScoreText;
        [SerializeField] private TMP_Text highScoreText;

        private void InitializeScoreUI(GameObject ctxObject)
        {
            currentScoreText.text = "0";
            highScoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        }
        
        private void UpdateScoreUI(int score)
        {
            currentScoreText.text = score.ToString();
        }

        private void UpdateHighscoreUI(int highscore)
        {
            highScoreText.text = highscore.ToString();
        }
        
        private void OnEnable()
        {
            EventManager.Subscribe<GameObject>(EventManager.Event.OnGameStarted, InitializeScoreUI);
            EventManager.Subscribe<int>(EventManager.Event.OnScored, UpdateScoreUI);
            EventManager.Subscribe<int>(EventManager.Event.OnNewHighscore, UpdateHighscoreUI);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<GameObject>(EventManager.Event.OnGameStarted, InitializeScoreUI);
            EventManager.Unsubscribe<int>(EventManager.Event.OnScored, UpdateScoreUI);
            EventManager.Unsubscribe<int>(EventManager.Event.OnNewHighscore, UpdateHighscoreUI);
        }
    }
}
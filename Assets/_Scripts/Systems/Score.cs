using SuikAR.Events;
using UnityEngine;

namespace SuikAR.Systems
{
    public class Score
    {
        public int CurrentScore { get; private set; }
        public int Highscore { get; private set; }

        public Score()
        {
            CurrentScore = 0;
            Highscore = PlayerPrefs.GetInt("Highscore", 0);
        }
        
        public static Score operator +(Score score, int value)
        {
            score.CurrentScore += value;
            EventManager.Invoke(EventManager.Event.OnScored, score.CurrentScore);
            
            if (score.CurrentScore > score.Highscore)
            {
                score.Highscore = score.CurrentScore;
                PlayerPrefs.SetInt("Highscore", score.Highscore);
                EventManager.Invoke(EventManager.Event.OnNewHighscore, score.Highscore);
            }
            
            return score;
        }
    }
}
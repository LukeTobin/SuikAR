using SuikAR.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SuikAR.Fruits
{
    public class NextFruitUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] public Image nextFruitSprite;

        private void ShowQueuedFruit(Fruit fruit)
        {
            nextFruitSprite.color = fruit.color;
        }

        private void OnEnable()
        {
            EventManager.Subscribe<Fruit>(EventManager.Event.OnFruitQueued, ShowQueuedFruit);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<Fruit>(EventManager.Event.OnFruitQueued, ShowQueuedFruit);
        }
    }
}
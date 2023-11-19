using UnityEngine;

namespace SuikAR.Fruits
{
    [CreateAssetMenu(fileName = "New Fruit", menuName = "New Fruit")]
    public class Fruit : ScriptableObject
    {
        public Color color = Color.white;
        public float scale = 0.1f;
        [Space]
        public Fruit combineFruit = null;
        public int combineScore = 1;
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SuikAR.Events;
using UnityEngine;

namespace SuikAR.Fruits
{
    public class FruitObject : MonoBehaviour
    {
        public Fruit Fruit { get; private set; }
        public List<FruitObject> Matched = new List<FruitObject>();

        private Rigidbody _rigidbody;
        private MeshRenderer _renderer;
        
        [Header("Settings")]
        [SerializeField] private float projectionForce = 40f;
        [SerializeField] private int matchAmount = 3;
        
        [Header("Debug")] 
        [SerializeField] private Fruit _fruit;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<MeshRenderer>();

            if (_fruit)
            {
                Initialize(_fruit, transform.position);
            }
        }

        public void Initialize(Fruit fruit, Vector3 position)
        {
            Fruit = fruit;
            Matched.Clear();
            transform.position = position;
            transform.localScale = new Vector3(fruit.scale, fruit.scale, fruit.scale);
            
            Material fruitMat = new Material(_renderer.material);
            fruitMat.color = fruit.color;
            _renderer.material = fruitMat;
            
            gameObject.SetActive(true);
        }

        public void ApplyForce(Vector3 direction)
        {
            _rigidbody.AddForce(direction * projectionForce, ForceMode.Force);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out FruitObject fruitObject))
            {
                if (Fruit != fruitObject.Fruit) return;
                
                if (!Matched.Contains(fruitObject))
                {
                    Matched.Add(fruitObject);
                }
                
                if(Matched.Count >= matchAmount - 1)
                {
                    EventManager.Invoke(EventManager.Event.OnFruitCombine, new FruitCombineData(this, Matched[0], Matched[1]));
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (Matched.Count <= 0) return;

            if (other.gameObject.TryGetComponent(out FruitObject fruitObject))
            {
                if (Fruit == fruitObject.Fruit && Matched.Contains(fruitObject)) Matched.Remove(fruitObject);
            }
        }
    }
}
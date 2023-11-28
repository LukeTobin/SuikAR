using System;
using SuikAR.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SuikAR.Fruits
{
    public class FruitInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FruitManager fruitManager;
        [SerializeField] private Camera xrCamera;

        [Header("Properties")] 
        [SerializeField] private float fruitSpawnOffset = 0.5f;
        [SerializeField] private float touchCooldownTime = 0.4f;

        private Controls controls;
        private bool touchCooldown = false;

        private void Awake()
        {
            controls = new Controls();
        }

        private void Initialize(GameObject ctxObject)
        {
            controls.Default.Enable();

            // Initialize a touch cooldown for sensitive phone screens
            touchCooldown = true;
            Invoke(nameof(ResetCooldown),touchCooldownTime);

            controls.Default.Touch.performed += ctx =>
            {
                if (ctx.control.device is Pointer device)
                {
                    OnPress(device.position.ReadValue());
                }
            };
        }

        private void OnPress(Vector3 screenPosition)
        {
            if (touchCooldown) return;
            
            // Calculate Spawn Position
            Vector3 cameraForward = xrCamera.transform.forward;
            float spawnDistance = fruitSpawnOffset;
            Vector3 spawnPosition = xrCamera.transform.position + (cameraForward * spawnDistance);
            
            // Get the next fruit to spawn
            FruitObject spawnedObject = fruitManager.GetCurrentFruit();
            if (spawnedObject != null)
            {
                spawnedObject.gameObject.transform.position = spawnPosition;
                spawnedObject.gameObject.SetActive(true);
                    
                spawnedObject.ApplyForce(spawnPosition);
            }
            
            // Cooldown flag to avoid screen bugs or spam abuse
            touchCooldown = true;
            Invoke(nameof(ResetCooldown),touchCooldownTime);
        }

        private void ResetCooldown() => touchCooldown = false;

        private void OnEnable()
        {
            EventManager.Subscribe<GameObject>(EventManager.Event.OnGameStarted, Initialize);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<GameObject>(EventManager.Event.OnGameStarted, Initialize);
            controls.Default.Disable();
        }
    }
}
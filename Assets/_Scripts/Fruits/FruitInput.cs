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

        private Controls controls;
        
        private void Awake()
        {
            controls = new Controls();

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
            // Get the forward direction of the camera
            Vector3 cameraForward = xrCamera.transform.forward;

            // Set the distance from the camera that you want to spawn the object
            float spawnDistance = fruitSpawnOffset;

            // Calculate the position in front of the camera
            Vector3 spawnPosition = xrCamera.transform.position + (cameraForward * spawnDistance);

            FruitObject spawnedObject = fruitManager.GetCurrentFruit();
            if (spawnedObject != null)
            {
                spawnedObject.gameObject.transform.position = spawnPosition;
                spawnedObject.gameObject.SetActive(true);
                    
                spawnedObject.ApplyForce(spawnPosition);
            }
        }

        private void OnEnable()
        {
            controls.Default.Enable();
        }

        private void OnDisable()
        {
            controls.Default.Disable();
        }
    }
}
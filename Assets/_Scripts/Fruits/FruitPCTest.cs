using System;
using UnityEngine;

namespace SuikAR.Fruits
{
    public class FruitPCTest : MonoBehaviour
    {
        [SerializeField] private FruitManager fruitManager;
        [SerializeField] private Camera xrCamera;
        
        private Controls controls;

        private void Awake()
        {
            controls = new Controls();
            /*controls.Default.Touch.performed += ctx =>
            {
                if (ctx.control.device is Pointer device)
                {
                    OnPress(device.position.ReadValue());
                }
            };*/
        }

        private void Update()
        {
            /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Get the camera's position and forward direction
                Vector3 cameraPosition = xrCamera.transform.position;
                Vector3 cameraForward = Camera.main.transform.forward;

                // Calculate the spawn position
                Vector3 spawnPosition = cameraPosition + cameraForward * distanceFromCamera;

                // Spawn the object in front of the camera
                GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            
                // Optionally, you can make the spawned object a child of the camera
                newObject.transform.parent = Camera.main.transform;
            }*/
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
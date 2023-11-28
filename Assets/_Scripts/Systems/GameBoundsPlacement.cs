using System.Collections.Generic;
using SuikAR.Events;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

namespace SuikAR.Systems
{
    public class GameBoundsPlacement : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private GameObject boundsPrefab;
        [SerializeField] private Vector3 spawnOffset;
        
        private Controls controls;
        private ARRaycastManager arRaycastManager;
        private ARPlaneManager arPlaneManager;
        private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        private void Awake()
        {
            arRaycastManager = GetComponent<ARRaycastManager>();
            arPlaneManager = GetComponent<ARPlaneManager>();
            
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
            var touchPosition = Pointer.current.position.ReadValue();

            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject ctxObject = Instantiate(boundsPrefab, hitPose.position + spawnOffset, hitPose.rotation);
                EventManager.Invoke(EventManager.Event.OnGameStarted, ctxObject);
                
                // XR Cleanup
                DeactivateARSetupTools();
                controls.Default.Disable();
            }
        }

        /// <summary>
        /// Deactivates all unused XR tools after setup has been complete, as they will no longer be needed during gameplay
        /// </summary>
        private void DeactivateARSetupTools()
        {
            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
            
            arRaycastManager.enabled = false;
            arPlaneManager.enabled = false;
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
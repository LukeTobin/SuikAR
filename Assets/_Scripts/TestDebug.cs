using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SuikAR
{
    public class TestDebug : MonoBehaviour
    {
        private Vector3 spawnPosition;
        private Rigidbody rb;
        
        public bool reset;
        public bool force;
        
        // Start is called before the first frame update
        void Start()
        {
            spawnPosition = transform.position;
            rb = GetComponent<Rigidbody>();
        }
    
        // Update is called once per frame
        void Update()
        {
            if (reset)
            {
                rb.velocity = Vector3.zero;
                transform.position = spawnPosition;
                reset = false;
            }

            if (force)
            {
                rb.AddForce(Vector3.down * 20f, ForceMode.Force);
                force = false;
            }
        }
    }
}


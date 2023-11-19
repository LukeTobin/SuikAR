using UnityEngine;

namespace SuikAR.Systems
{
    public class GravitationalPull : MonoBehaviour
    {
        [SerializeField] private float gravitationalForce = 9.81f;
        [SerializeField] private float gravitationalDeadzone = 0.3f; // gravity strength should be lower the closer to the distance to this value

        private void FixedUpdate()
        {
            foreach (var collider in Physics.OverlapBox(transform.position, transform.localScale))
            {
                collider.TryGetComponent(out Rigidbody rb);
                if (rb)
                {
                    Vector3 direction = transform.position - collider.transform.position;
                    float distance = direction.magnitude;

                    if (distance > gravitationalDeadzone)
                    {
                        // Calculate gravitational force
                        float forceMagnitude = gravitationalForce * rb.mass / Mathf.Pow(distance, 2);
                        Vector3 force = direction.normalized * forceMagnitude;

                        // Apply force to the object
                        rb.AddForce(force);
                    }
                    else if (distance <= gravitationalDeadzone && distance > 0)
                    {
                        // Calculate reduced force within the dead zone
                        float forceMagnitude = gravitationalForce * rb.mass / Mathf.Pow(gravitationalDeadzone, 2);
                        float distanceFactor = Mathf.InverseLerp(0, gravitationalDeadzone, distance);
                        float reducedForceMagnitude = Mathf.Lerp(0, forceMagnitude, distanceFactor);
                        Vector3 force = direction.normalized * reducedForceMagnitude;

                        // Apply force to the object
                        rb.AddForce(force);
                    }
                }
            }
        }
    } 
}
using UnityEngine;

namespace SpaceShooter
{
    public class TurretAutoAIms : MonoBehaviour
    {
        public Transform target;
        public float turretAimSpeed = 500.0f;
        Quaternion newRotation;
        float orientTransform;
        float orientTarget;

        private void Start()
        {
           
        }
        
        void Update()
        {
            orientTransform = transform.position.x;
            orientTarget = target.position.x;
            if (orientTransform > orientTarget)
            {
                newRotation = Quaternion.LookRotation(transform.position - target.position, -Vector3.up);
            }
            else
            {
                newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.up);
            }

            newRotation.x = 0.0f;
            newRotation.y = 0.0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turretAimSpeed);
        }
    }
}
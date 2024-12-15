using H00N.Extensions;
using UnityEngine;

namespace ProjectCoin.Units
{
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] float acceleration = 0f;
        private float maxSpeed = 0f;
        private float velocity = 0f;
        private Vector2 currentDestination = Vector2.zero;

        private void FixedUpdate()
        {
            Vector3 currentPosition = transform.position;
            Vector3 directionVector = currentDestination - currentPosition.PlaneVector();
            if(directionVector.sqrMagnitude <= 0.1f)
            {
                velocity = 0f;   
                return;
            }

            velocity += acceleration * Time.fixedDeltaTime;
            velocity = Mathf.Min(velocity, maxSpeed);
            
            Vector3 direction = directionVector.normalized;
            transform.position += direction * (velocity * Time.fixedDeltaTime);
        }

        public void SetMaxSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }

        public void SetAcceleration(float acceleration)
        {
            this.acceleration = acceleration;
        }

        public void SetDestination(Vector2 destination)
        {
            currentDestination = destination;
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

namespace AI.SteeringBehaviours
{
    public class SteeringBehaviours
    {
        public static Vector2 Seek(Vector2 agentPosition, Vector2 agentVelocity, Vector2 targetPosition, float maxSpeed, float maxForce)
        {
            Vector2 desired;
            Vector2 steer;
            desired = (targetPosition - agentPosition).normalized * maxSpeed;
            steer = (desired - agentVelocity).normalized * maxForce;

            return steer;
        }

        public static Vector2 Flee(Vector2 agentPosition, Vector2 agentVelocity, Vector2 targetPosition, float maxSpeed, float maxForce)
        {
            Vector2 desired;
            Vector2 steer;
            desired = (agentPosition - targetPosition).normalized * maxSpeed;
            steer = (desired - agentVelocity).normalized * maxForce;

            return steer;
        }

        public static Vector2 Arrive(Vector2 agentPosition, Vector2 targetPosition, float maxSpeed, float maxForce, float arriveRadius, Vector2 agentVelocity)
        {
            Vector2 desired;
            Vector2 steer;

            desired = (targetPosition - agentPosition).normalized * maxSpeed;

            // Calcular velocidad en función a la distancia
            if (Vector3.Distance(agentPosition, targetPosition) < arriveRadius)
            {
                desired *= Vector3.Distance(agentPosition, targetPosition) / arriveRadius;
            }

            steer = Vector2.ClampMagnitude(desired - agentVelocity, maxForce);

            return steer;
        }

        public static Vector2 Wander(Vector2 agentPosition, Vector2 agentVelocity, float wanderDistance,
                                  float wanderRadius, float maxSpeed, float maxForce)
        {
            Vector2 desired;
            Vector2 steer;
            float angle = Random.value * 360;
            Vector2 v1 = agentVelocity.normalized * wanderDistance;            
            Vector2 v2 = RotateVector(agentVelocity, angle).normalized * wanderRadius;
            Vector2 p = agentPosition + v1 + v2;

            desired = (p - agentPosition).normalized * maxSpeed;
            steer = (desired - agentVelocity).normalized * maxForce;

            return steer;
        }

        public static Vector2 RotateVector(Vector2 vector, float angle)
        {
            Vector2 rotatedVector = new Vector2();
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            rotatedVector.x = vector.x * cos - vector.y * sin;
            rotatedVector.y = vector.x * sin + vector.y * cos;

            return rotatedVector;
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BeeHunter.Attributes {
    public class Move : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] protected float speedMoveNormal = 2;

        [Header("NavMeshAgent")]
        [SerializeField] Transform target;
        [SerializeField] protected float maxSpeed = 6f;
        [SerializeField] protected float maxAceleration = 50;
        [SerializeField] protected float maxNavPathLenght = 40f;

        #endregion

        #region functions
        protected NavMeshAgent navMeshAgent;

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            if (!CanMoveTo(destination)) return;
            MoveTo(destination, maxSpeed, speedFraction);
        }

        public virtual bool CanMoveTo(Vector3 destination)
        {
            //if(!m_usedNavMeshMovement) return true;

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;

            //modificate the destination
            if (GetPathLength(path) > maxNavPathLenght) return false;

            return true;
        }

        /// <summary>
        /// Move the player with navMeshAgent to destination 
        /// </summary>
        public virtual void MoveTo(Vector3 destination, float maxSpeed, float SpeedFraction)
        {
            //navmeshMovement
            var tfPosition = transform.position;
            if (maxSpeed > 50)
            {
                navMeshAgent.acceleration = maxSpeed;
            }
            else
            {
                navMeshAgent.acceleration = maxAceleration;
            }


            try
            {
                navMeshAgent.destination = destination;
            }
            catch (Exception e)
            {
                navMeshAgent.destination = tfPosition;
                print(e);
            }

            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(SpeedFraction);
            navMeshAgent.isStopped = false;

        }

        public void Cancel()
        {
            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.isStopped = true;
            }
        }

#endregion

        #region private function

        float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }
        #endregion
    }
}

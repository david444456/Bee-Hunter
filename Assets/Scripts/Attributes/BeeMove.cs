using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BeeHunter.Attributes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BeeMove : Move
    {
        [Header("Bee move")]
        [SerializeField] Transform _transformBeeMesh;

        Vector3 _destionationY;
        float _speedFractionY;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {

            if ( _transformBeeMesh.transform.position != _destionationY)
            {
                _transformBeeMesh.transform.localPosition = Vector3.MoveTowards(
                                                        _transformBeeMesh.transform.localPosition, _destionationY, _speedFractionY*Time.deltaTime);
            }
        }

        public float GetActualSpeed() => navMeshAgent.speed;

        public void StartMoveActionUpDirection(Vector3 dest, float timeInSeconds) {
            _destionationY = dest;
            _speedFractionY = Vector3.Distance(_destionationY, _transformBeeMesh.transform.localPosition) / timeInSeconds;
        }
    }
}

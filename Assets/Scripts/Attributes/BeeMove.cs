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
        [SerializeField] float _heightToUp;
        [SerializeField] Transform _transformBeeMesh;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BeeHunter.Slots
{
    public class CollisionDetecting : MonoBehaviour
    {

        [SerializeField] UnityEvent<GameObject> NewObjectCollision;
        [SerializeField] string _nameTagObjecttoCollision = "Flower";

        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (_nameTagObjecttoCollision == other.tag) {
                NewObjectCollision.Invoke(other.gameObject);
            }
        }

    }
}

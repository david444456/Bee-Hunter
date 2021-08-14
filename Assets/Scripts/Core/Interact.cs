using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class Interact : MonoBehaviour
    {
        public Action<GameObject> SelectedEvent = delegate { };
        public Action<GameObject> DeselectedEvent = delegate { };

        [SerializeField] private float maxDistance = 1.5f;

        LayerMask _layerSelected;

        private GameObject _lastSelectedGO = null;
        private Transform _transformCamera = null;
        private bool _selectOneObject = false;

        void Start()
        {
            _layerSelected = LayerMask.GetMask("Ray Detect");

            _transformCamera = Camera.main.gameObject.transform;
        }

        void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(_transformCamera.position,
                _transformCamera.forward,
                out hit, maxDistance,
                _layerSelected))
            {
                if (hit.collider.GetComponent<Selectable>() != null)
                {
                    _lastSelectedGO = hit.collider.gameObject;
                    SelectedEvent.Invoke(_lastSelectedGO);
                    _selectOneObject = true;
                    print("Selecteable");
                }

            }
            else if (_lastSelectedGO != null)
            {
                DeselectItemObject();
            }
            else if (_selectOneObject && _lastSelectedGO == null)
            {
                DeselectItemObject();
            }
            Debug.DrawLine(_transformCamera.position, Camera.main.gameObject.transform.forward * maxDistance, Color.red);
        }

        private void DeselectItemObject()
        {
            DeselectedEvent.Invoke(_lastSelectedGO);
            _lastSelectedGO = null;
            _selectOneObject = false;
        }
    }
}

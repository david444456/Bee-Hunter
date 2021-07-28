using BeeHunter.Slots;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlInfoBee : MonoBehaviour
    {
        public Action<bool> ChangeStateBeeInContainerEvent = delegate { };
        [SerializeField] BeeItem _actualBeeItem;
        
        private ControlItemObject controlItem;

        private bool _beeInContainer = false;
        private Container _mainContainer;
        private Transform[] _dimentionsContainer;
        private GameObject _diaperGO;

        void Start()
        {
            controlItem = GetComponent<ControlItemObject>();

            controlItem.DesactiveObjectEvent += DesactiveObjectInformToMainContainer;
            //_actualBeeItem = GetComponentInParent<ControlItemObject>().GetActualItem();
        }

        public void ReceiveInformationFromContainer(Container newCont, Transform[] dimentions, GameObject GOdiaper) {
            _beeInContainer = true;
            ChangeStateBeeInContainerEvent.Invoke(true);
            _mainContainer = newCont;
            _dimentionsContainer = dimentions;
            _diaperGO = GOdiaper;
        }

        public Container GetActualContainer() => _mainContainer;

        public Transform[] GetLimitsTransform() => _dimentionsContainer;

        public BeeItem GetActualBeeItem() => _actualBeeItem;

        private void DesactiveObjectInformToMainContainer()
        {
            controlItem.DesactiveObjectEvent -= DesactiveObjectInformToMainContainer;

            if (_mainContainer != null)
                _mainContainer.RemoveObjectFromList(this);
        }
    }
}

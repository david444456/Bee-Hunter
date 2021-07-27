using BeeHunter.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlInfoBee : MonoBehaviour
    {
        [SerializeField] BeeItem _actualBeeItem;

        ControlItemObject controlItem;
        Container _mainContainer;

        void Start()
        {
            controlItem = GetComponent<ControlItemObject>();

            controlItem.DesactiveObjectEvent += DesactiveObjectInformToMainContainer;
            //_actualBeeItem = GetComponentInParent<ControlItemObject>().GetActualItem();
        }

        public void ReceiveInformationFromContainer(Container newCont) {
            _mainContainer = newCont;
        }

        public BeeItem GetActualBeeItem() => _actualBeeItem;

        private void DesactiveObjectInformToMainContainer()
        {
            if(_mainContainer != null)
                _mainContainer.RemoveObjectFromList(this);
        }
    }
}

using BeeHunter.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlFlower : MonoBehaviour, IDesactiveItemObject
    {

        private Container _mainContainer;
        private ControlItemObject controlItem;

        void Start()
        {
            controlItem = GetComponent<ControlItemObject>();

            controlItem.DesactiveObjectEvent += DesactiveObjectInformToMainContainer;
        }

        public void DesactiveObjectInformToMainContainer()
        {

            if (_mainContainer != null)
                _mainContainer.RemoveFlowerObjectFromList(this.gameObject);
        }
    }
}

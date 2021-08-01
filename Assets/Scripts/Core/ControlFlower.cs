using BeeHunter.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlFlower : MonoBehaviour, IDesactiveItemObject
    {
        [SerializeField] int _maxUsesFlower = 3;

        private Container _mainContainer;
        private ControlItemObject controlItem;

        private int _countUses = 0;

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

        public bool NewJobFinishedWithThisFlower_ReturnIfFlowerDie() {
            //desactive can player touch this object
            if (_countUses <= 0) GetComponent<Collider>().enabled = false;

            //working with uses
            _countUses++;
            if (_countUses >= _maxUsesFlower) {

                print("Destroy");
                return true;
            }

            return false;
        }
    }
}

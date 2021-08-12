using BeeHunter.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlFlower : MonoBehaviour, IDesactiveItemObject
    {
        [SerializeField] FlowerItem _itemFlower;
        int _maxUsesFlower = 0;

        private Container _mainContainer;
        private ControlItemObject controlItem;

        private int _countUses = 0;

        void Start()
        {
            controlItem = GetComponent<ControlItemObject>();

            _maxUsesFlower = _itemFlower.GetMaxUsesFlower();

            controlItem.DesactiveObjectEvent += DesactiveObjectInformToMainContainer;
        }

        public FlowerItem GetActualFlowerItem() => _itemFlower;

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
                Destroy(gameObject, 0.1f);
                print("Active animation, and with animation destroy this object");
                return true;
            }

            return false;
        }
    }
}

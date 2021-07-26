using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player {
    public class InteractInventory : MonoBehaviour
    {
        [SerializeField] Item _newItemTouch;

        PlayerInventory _playerInventory;
        PlayerUI _playerUI;
        PlayerBeeInput _playerInput;

        ControlItemObject _actualControlItemTouch;

        void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerUI = GetComponent<PlayerUI>();
            _playerInput = GetComponent<PlayerBeeInput>();

            //input
            _playerInput.rightClickMouseEvent += InteractWithItemTouch;
            _playerInput.leftClickMouseEvent += PushItemInventoryToOutside;
        }

        private void OnTriggerEnter(Collider other)
        {
            print("Col" + other.gameObject.name);
            if (other.gameObject.tag == "Item")
            {
                _actualControlItemTouch = other.gameObject.GetComponent<ControlItemObject>();

                _newItemTouch = _actualControlItemTouch.GetActualItem();

                _playerUI.ChangeStateTouchNewItemUI(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Item")
            {
                _newItemTouch = null;
                _playerUI.ChangeStateTouchNewItemUI(false);
            }
        }

        private void PushItemInventoryToOutside() {
            Item it = _playerInventory.GetActualItem();
            if (it.GetActualIndexItem() == 0) return;

            //push item
            Instantiate( it.GetActualGOPrefab(), transform.position, Quaternion.identity);
            //substract value in the inventory
        }

        private void InteractWithItemTouch() {
            if (_newItemTouch == null) return;
            print(_newItemTouch.GetNameItem());

            if (_playerInventory.PickUpNewItem(_newItemTouch)) {
                _actualControlItemTouch.DesactiveObject();
                _newItemTouch = null;
            }
        }
    }
}

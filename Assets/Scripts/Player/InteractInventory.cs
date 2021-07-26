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

        void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerUI = GetComponent<PlayerUI>();
            _playerInput = GetComponent<PlayerBeeInput>();

            //input
            _playerInput.rightClickMouseEvent += InteractWithItemTouch;
        }

        private void OnTriggerEnter(Collider other)
        {
            print("Col" + other.gameObject.name);
            if (other.gameObject.tag == "Item")
            {
                _newItemTouch = other.gameObject.GetComponent<ControlItemObject>().GetActualItem();
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

        private void InteractWithItemTouch() {
            if (_newItemTouch == null) return;
            print(_newItemTouch.GetNameItem());
            _playerInventory.PickUpNewItem(_newItemTouch);
        }
    }
}

using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player {
    public class InteractInventory : MonoBehaviour
    {

        [SerializeField] Item _newItemTouch;

        [Header("Inventory interact")]
        [SerializeField] Transform _itemTFPositionSpawn;
        [SerializeField] float _forceSpawn;
        [SerializeField] float _timeMaxBetweenPickItem = 0.5f;

        PlayerInventory _playerInventory;
        PlayerUI _playerUI;
        PlayerBeeInput _playerInput;

        ControlItemObject _actualControlItemTouch;

        private float _timeLastPickItem = 0;

        void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerUI = GetComponent<PlayerUI>();
            _playerInput = GetComponent<PlayerBeeInput>();

            //input
            _playerInput.rightClickMouseEvent += InteractWithItemTouch;
            _playerInput.leftClickMouseEvent += PushItemInventoryToOutside;
        }

        private void Update()
        {
            _timeLastPickItem += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
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
            GameObject GONewItem = Instantiate( it.GetActualGOPrefab(), _itemTFPositionSpawn.transform.position, Quaternion.identity);
            GONewItem.GetComponent<Rigidbody>().AddForce(transform.forward * _forceSpawn, ForceMode.Force);
            //substract value in the inventory
            _playerInventory.SubstractValueInInventory();
        }

        private void InteractWithItemTouch() {
            if (_newItemTouch == null) return;
            if (_timeMaxBetweenPickItem > _timeLastPickItem) return;

            if (_playerInventory.PickUpNewItem(_newItemTouch)) {
                _actualControlItemTouch.DesactiveObject();
                _playerUI.ChangeStateTouchNewItemUI(false);
                _newItemTouch = null;
            }

            //time 
            _timeLastPickItem = 0;

            //desactive and active the collider
            GetComponent<CharacterController>().enabled = false;
            GetComponent<CharacterController>().enabled = true;
        }
    }
}

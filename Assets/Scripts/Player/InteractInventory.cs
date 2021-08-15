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
        Interact interactPlayer;

        ControlItemObject _actualControlItemTouch;

        private float _timeLastPickItem = 0;

        void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerUI = GetComponent<PlayerUI>();
            _playerInput = GetComponent<PlayerBeeInput>();
            interactPlayer = GetComponent<Interact>();

            //input
            _playerInput.rightClickMouseEvent += InteractWithItemTouch;
            _playerInput.leftClickMouseEvent += PushItemInventoryToOutside;

            //event interact
            interactPlayer.SelectedEvent += StartInteractWithObject;
            interactPlayer.DeselectedEvent += FinishInteractWithObject;
        }

        private void Update()
        {
            _timeLastPickItem += Time.deltaTime;
        }

        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Item")
            {
                _actualControlItemTouch = other.gameObject.GetComponent<ControlItemObject>();

                _newItemTouch = _actualControlItemTouch.GetActualItem();

                _playerUI.ChangeStateTouchSomethingUI(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Item")
            {
                ExitToZoneInteractObject();
            }
        }*/

        public void DestroyObjectItem(GameObject other) {
            StartCoroutine(AfterDestroyObjectWhenTouch());
        }

        private void StartInteractWithObject(GameObject GOCollider)
        {
            if (GOCollider.tag == "Item")
            {
                _actualControlItemTouch = GOCollider.gameObject.GetComponent<ControlItemObject>();

                _newItemTouch = _actualControlItemTouch.GetActualItem();

                _playerUI.ChangeStateTouchSomethingUI(true);
            }
        }

        private void FinishInteractWithObject(GameObject GOCollider)
        {
            if (GOCollider.gameObject == null) {
                ExitToZoneInteractObject();
            }
            else if (GOCollider.gameObject.tag == "Item")
            {
                ExitToZoneInteractObject();
            }
        }

        private void ExitToZoneInteractObject()
        {
            _newItemTouch = null;
            _playerUI.ChangeStateTouchSomethingUI(false);
        }

        private void PushItemInventoryToOutside() {
            Item it = _playerInventory.GetActualItem();
            if (it.GetActualIndexItem() == 0) return;

            //push item
            GameObject GONewItem = Instantiate( it.GetActualGOPrefab(), _itemTFPositionSpawn.transform.position, Quaternion.identity);

            //ramdon force
            float _randomForce = UnityEngine.Random.Range(0.5f, 0.8f);

            //force
            GONewItem.GetComponent<Rigidbody>().AddForce(transform.forward * _randomForce * _forceSpawn , ForceMode.Force);



            //substract value in the inventory
            _playerInventory.SubstractValueInInventory();
        }

        private void InteractWithItemTouch() {
            if (_newItemTouch == null) return;
            if (_timeMaxBetweenPickItem > _timeLastPickItem) return;

            if (_playerInventory.PickUpNewItem(_newItemTouch)) {
                _actualControlItemTouch.DesactiveObject();
                _playerUI.ChangeStateTouchSomethingUI(false);
                _newItemTouch = null;
            }

            //time 
            _timeLastPickItem = 0;

            //desactive and active the collider
            GetComponent<CharacterController>().enabled = false;
            GetComponent<CharacterController>().enabled = true;
        }

        IEnumerator AfterDestroyObjectWhenTouch() {
            yield return new WaitForEndOfFrame();
            ExitToZoneInteractObject();
        }
    }
}

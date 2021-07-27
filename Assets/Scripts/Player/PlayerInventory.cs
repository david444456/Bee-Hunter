using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeeHunter.Core;

namespace BeeHunter.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] int maxItemsInventory = 3;
        [SerializeField] Item emptyItem;

        Item[] _actualItems;
        int[] _actualCountItemsByType;

        int _actualSelectItem = 0;
        int _maxCountItemByType = 0;

        PlayerBeeInput _playerInput;
        PlayerUI _playerUI;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerBeeInput>();
            _playerUI = GetComponent<PlayerUI>();

            _actualItems = new Item[maxItemsInventory];
            _actualCountItemsByType = new int[maxItemsInventory];

            for (int i = 0; i < _actualItems.Length; i++)
            {
                _actualItems[i] = emptyItem;
            }
        }

        void Start()
        {
            //update UI
            _playerInput.alphaNumbersButtonEvent += ChangeSelectItem;
        }

        /// <summary>
        /// This method add a new item in the inventory, returns true if it can put the item, 
        /// </summary>
        /// <param name="item">The new item</param>
        public bool PickUpNewItem(Item item) {
            if (CheckThisItemExistInInventoryAndAdd(item)) return true;
            else if (CheckInventoryHaveOneSlotEmpty())
            {
                AddItemInEmptySlot(item);
                return true;
            }
            else Debug.Log("You have not space in the inventory.");

            return false;
        }

        public Item GetActualItem() => _actualItems[_actualSelectItem];

        public void SubstractValueInInventory() {
            _actualCountItemsByType[_actualSelectItem]--;
            if (_actualCountItemsByType[_actualSelectItem] <= 0)
            { 
                _actualItems[_actualSelectItem] = emptyItem;
                _playerUI.UpdateNewItemInInventory(_actualSelectItem, emptyItem.GetActualSpriteByItem());
            }

            UpdateTextInventory(_actualSelectItem);

        }

        private bool CheckThisItemExistInInventoryAndAdd(Item item) {
            for (int i = 0; i < _actualItems.Length; i++) {
                Item it = _actualItems[i];
                if (item.GetActualIndexItem() == it.GetActualIndexItem()) {
                    AddItemInExistingIndex(i);
                    return true;
                }
            }
            return false;
        }

        private bool CheckInventoryHaveOneSlotEmpty() {
            for (int i = 0; i < _actualItems.Length; i++)
            {
                if (_actualCountItemsByType[i] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddItemInExistingIndex(int index)
        {
            if(_maxCountItemByType > _actualCountItemsByType[index])
                _actualCountItemsByType[index]++;

            //ui
            UpdateTextInventory(index);
        }

        private void AddItemInEmptySlot(Item item) {
            for (int i = 0; i < _actualItems.Length; i++)
            {
                if (_actualCountItemsByType[i] == 0) {
                    //new item
                    _actualItems[i] = item;
                    _actualCountItemsByType[i]++;
                    
                    //ui
                    _playerUI.UpdateNewItemInInventory(i, item.GetActualSpriteByItem());
                    UpdateTextInventory(i);
                    return;
                }
            }
        }

        private void ChangeSelectItem(int newSelectItem) {
            
            if(InRange(newSelectItem)) _actualSelectItem = newSelectItem;
            //ui
            _playerUI.ChangeSelectionUIInventory(newSelectItem);
        }

        private void UpdateTextInventory(int index) => _playerUI.UpdateNewTextCounItemInInventory(index, _actualCountItemsByType[index].ToString());

        private bool InRange(int value) => value >= 0 && value < maxItemsInventory;
    }
}
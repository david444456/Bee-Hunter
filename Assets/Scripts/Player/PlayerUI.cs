using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeHunter.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] Image[] imagesByItemsInInventory;
        [SerializeField] Text[] textCountItemsInInventory;
        [SerializeField] RectTransform transformImageActualSelectInventory;

        [Header("Interact")]
        [SerializeField] GameObject GOInteractWithObjectUI;

        [Header("Coin")]
        [SerializeField] Text textCoinPlayer;

        public void ChangeTextCoin(int newCoin) => textCoinPlayer.text = newCoin.ToString();

        public void ChangeSelectionUIInventory(int index) {
            transformImageActualSelectInventory.anchoredPosition = imagesByItemsInInventory[index].rectTransform.anchoredPosition;
        }

        public void UpdateNewItemInInventory(int index, Sprite sprite) {
            imagesByItemsInInventory[index].sprite = sprite;
        }

        public void UpdateNewTextCounItemInInventory(int index, string newTextValue) {
            textCountItemsInInventory[index].text = newTextValue;
        }

        public void ChangeStateTouchNewItemUI(bool newValue) => GOInteractWithObjectUI.SetActive(newValue);
    }
}

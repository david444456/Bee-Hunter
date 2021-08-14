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
        [SerializeField] GameObject GOInteractWithStoreUI;

        [Header("Coin")]
        [SerializeField] Text textCoinPlayer;

        [Header("Movement")]
        [SerializeField] Slider sliderMoveStamina;

        [Header("Health")]
        [SerializeField] Slider sliderHealth;

        public void ChangeHealthSlider(float newValue) => sliderHealth.value = newValue;

        public void ChangeMaxHealthSlider(int maxValue) => sliderHealth.maxValue = maxValue;

        public void ChangeMaxMoveStaminaSlider(int maxValue) => sliderMoveStamina.maxValue = maxValue;

        public void ChangeValueStaminaSlider(float newValue) => sliderMoveStamina.value = newValue;

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

        public void ChangeStateTouchSomethingUI(bool newValue) => GOInteractWithObjectUI.SetActive(newValue);

        public void ChangeStateTouchStoreUI(bool newValue) => GOInteractWithStoreUI.SetActive(newValue);
    }
}

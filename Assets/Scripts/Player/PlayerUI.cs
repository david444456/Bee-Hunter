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

        [Header("Interact")]
        [SerializeField] GameObject GOInteractWithObjectUI;

        void Start()
        {

        }

        public void UpdateNewItemInInventory(int index, Sprite sprite) {
            imagesByItemsInInventory[index].sprite = sprite;
        }

        public void ChangeStateTouchNewItemUI(bool newValue) => GOInteractWithObjectUI.SetActive(newValue);
    }
}

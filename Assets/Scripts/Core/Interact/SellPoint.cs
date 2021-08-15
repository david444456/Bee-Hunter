using BeeHunter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class SellPoint : MonoBehaviour
    {

        PlayerCoin coin;
        InteractInventory interactInventory;

        void Start()
        {
            coin = FindObjectOfType<PlayerCoin>();
            interactInventory = coin.GetComponent<InteractInventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Honey")
            {
                ControlItemObject controlItemObject = other.GetComponentInParent<ControlItemObject>();
                int price = controlItemObject.GetActualItem().GetCurrentPrice();

                //coin
                coin.SetAugmentCoin(price);

                //item
                controlItemObject.DesactiveObject();

                //inventory errors
                interactInventory.DestroyObjectItem(controlItemObject.gameObject);
            }
        }

    }
}

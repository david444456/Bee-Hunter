using BeeHunter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class SellPoint : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleNewItemSell;

        PlayerCoin _controlMainCoin;
        InteractInventory _interactInventory;

        void Start()
        {
            _controlMainCoin = FindObjectOfType<PlayerCoin>();
            _interactInventory = _controlMainCoin.GetComponent<InteractInventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Honey"))
            {
                //var
                ControlItemObject controlItemObject = GetControlItemObjectFromCollider(other);
                int actualItemCost = GetActualItemCost(controlItemObject);

                SetInformationAndActiveEffect(controlItemObject.transform.position, actualItemCost);
                DesactiveItemFromObjectsAndData(controlItemObject);
            }
        }

        private void DesactiveItemFromObjectsAndData(ControlItemObject controlItemObject)
        {
            //item
            controlItemObject.DesactiveObject();

            //update the player inventory
            _interactInventory.DestroyObjectItem(controlItemObject.gameObject);
        }

        private void SetInformationAndActiveEffect(Vector3 PositionToSpawnEffect, int priceActualItem)
        {
            //coin
            _controlMainCoin.SetAugmentCoin(priceActualItem);

            //effect
            ActiveEffectInPosition(PositionToSpawnEffect);
        }

        private void ActiveEffectInPosition(Vector3 PositionToSpawnEffect)
        {
            _particleNewItemSell.transform.position = PositionToSpawnEffect;
            _particleNewItemSell.Play();
        }

        private ControlItemObject GetControlItemObjectFromCollider(Collider other) => other.GetComponentInParent<ControlItemObject>();

        private int GetActualItemCost(ControlItemObject controlItemObject) => controlItemObject.GetActualItem().GetCurrentPrice();
    }
}

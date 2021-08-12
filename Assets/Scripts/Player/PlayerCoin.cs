using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player
{
    public class PlayerCoin : MonoBehaviour
    {
        [SerializeField] int _initialCoins = 0;
        PlayerUI playerUI;

        private int _actualCoin = 0;

        private void Start()
        {
            playerUI = GetComponent<PlayerUI>();

            SetAugmentCoin(_initialCoins);
        }

        public int GetActualCoin { get => _actualCoin; }

        public void SetAugmentCoin(int augmentValue)
        {
            _actualCoin += augmentValue;
            playerUI.ChangeTextCoin(_actualCoin);
        }

        public bool GetCanBuyItemWithPrice(int newPrice) {
            return newPrice <= _actualCoin;
        }
    }
}

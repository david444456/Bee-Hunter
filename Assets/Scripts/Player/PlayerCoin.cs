using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player
{
    public class PlayerCoin : MonoBehaviour
    {

        PlayerUI playerUI;

        private int _actualPremiumCoin = 0;

        private void Start()
        {
            playerUI = GetComponent<PlayerUI>();
        }

        public int GetActualCoin { get => _actualPremiumCoin; }

        public void SetAugmentCoin(int augmentValue)
        {
            _actualPremiumCoin += augmentValue;
            playerUI.ChangeTextCoin(_actualPremiumCoin);
        }
    }
}

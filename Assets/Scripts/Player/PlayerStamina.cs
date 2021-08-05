using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player
{
    public class PlayerStamina : MonoBehaviour
    {
        [SerializeField] int _maxStamina = 50;

        float _actualStamina = 0;
        PlayerUI playerUI;

        void Start()
        {
            playerUI = GetComponent<PlayerUI>();

            playerUI.ChangeMaxMoveStaminaSlider(_maxStamina);

            ChangeValueStamina(_maxStamina);
        }

        void Update()
        {
            if (_actualStamina < _maxStamina) ChangeValueStamina(Time.deltaTime);
        }

        public bool CanRun() => _actualStamina > 0;

        public void SubstractStamina(float newValue) => ChangeValueStamina(newValue);

        private void ChangeValueStamina(float newValue)
        {
            _actualStamina += newValue;
            playerUI.ChangeValueStaminaSlider(_actualStamina);
        }
    }
}
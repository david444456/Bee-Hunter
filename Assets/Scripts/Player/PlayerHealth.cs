using BeeHunter.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Player
{
    public class PlayerHealth : Health
    {
        PlayerUI playerUI;

        void Start()
        {
            playerUI = GetComponent<PlayerUI>();

            playerUI.ChangeMaxHealthSlider(initialHealthPoints);
            playerUI.ChangeHealthSlider(GetHealthPoints());
        }

        public override void TakeDamage(GameObject instigator, float damage)
        {
            base.TakeDamage(instigator, damage);
            playerUI.ChangeHealthSlider(GetHealthPoints());
        }
    }
}

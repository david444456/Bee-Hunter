using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    [CreateAssetMenu(fileName = "Honey 1", menuName = "Items/NewHoney", order = 0)]
    public class HoneyItem : Item
    {
        [Header("Honey")]
        [SerializeField] int _costThisHoney = 5;

        public int GetCostHoney() => _costThisHoney;

        public override string GetNameItem() => this.name;
    }
}


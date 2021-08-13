using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    [CreateAssetMenu(fileName = "Bee 1", menuName = "Items/NewBee", order = 0)]
    public class BeeItem : Item
    {
        [Header("Bee")]
        [SerializeField] TypeBee _typeBee;
        [SerializeField] GameObject _prefabHoney;
        public override string GetNameItem() => "BEE";

        public TypeBee GetTypeBee() => _typeBee;

        public GameObject GetGOHoney() => _prefabHoney;
    }

    public enum TypeBee { 
        Normal,
        Ghost,
        Aggressive
    }
}

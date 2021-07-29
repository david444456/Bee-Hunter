using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    [CreateAssetMenu(fileName = "Flower 1", menuName = "Items/NewFlower", order = 0)]
    public class FlowerItem : Item
    {
        public override string GetNameItem() => this.name;

    }
}

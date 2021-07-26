using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bee 1", menuName = "Items/NewBee", order = 0)]
public class BeeItem : Item
{
    public override string GetNameItem() => "BEE";
}

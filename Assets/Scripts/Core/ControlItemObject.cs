using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlItemObject : MonoBehaviour
    {
        [SerializeField] Item _actualItem;

        public Item GetActualItem() => _actualItem;

        public void DesactiveObject()
        {
            Destroy(gameObject);
        }
    }
}

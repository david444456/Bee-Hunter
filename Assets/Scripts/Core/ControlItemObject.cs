using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlItemObject : MonoBehaviour
    {
        public Action DesactiveObjectEvent = delegate { };        
        [SerializeField] Item _actualItem;

        public Item GetActualItem() => _actualItem;

        public void DesactiveObject()
        {
            DesactiveObjectEvent.Invoke();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlItemObject : MonoBehaviour
    {
        [SerializeField] Item _actualItem;

        void Start()
        {

        }

        public Item GetActualItem() => _actualItem;
    }
}

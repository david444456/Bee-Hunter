using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BeeHunter.Player
{
    public class PlayerBeeInput : MonoBehaviour
    {
        public Action<int> alphaNumbersButtonEvent = delegate { };
        public Action rightClickMouseEvent = delegate { };

        private PlayerActionControls playerActionControls;

        private void Awake()
        {
            playerActionControls = new PlayerActionControls();
        }

        private void OnEnable()
        {
            playerActionControls.Enable();
        }

        void Update()
        {
            CheckTouchButtonsInventory();
            CheckTouchRightClickMouse();
        }

        //inputs
        private bool OneAlphaInput() => playerActionControls.Player.Alpha1.ReadValue<float>() != 0.0f;
        private bool TwoAlphaInput() => playerActionControls.Player.Alpha2.ReadValue<float>() != 0.0f;
        private bool ThreeAlphaInput() => playerActionControls.Player.Alpha3.ReadValue<float>() != 0.0f;
        private bool RightClickMouseInput() => playerActionControls.Player.Fire.ReadValue<float>() != 0.0f;

        //checks
        private void CheckTouchButtonsInventory() {
            if (OneAlphaInput()) alphaNumbersButtonEvent.Invoke(0);
            else if(TwoAlphaInput()) alphaNumbersButtonEvent.Invoke(1);
            else if (ThreeAlphaInput()) alphaNumbersButtonEvent.Invoke(2);
        }

        private void CheckTouchRightClickMouse() {
            if (RightClickMouseInput()) rightClickMouseEvent.Invoke();
        }
    }
}

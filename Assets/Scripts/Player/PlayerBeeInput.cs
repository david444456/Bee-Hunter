using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BeeHunter.Player
{
    public class PlayerBeeInput : MonoBehaviour
    {
        [SerializeField] float _maxTimeBetweenLeftClick = 1;
        [SerializeField] float _limitBetweenTouchUpDownButton = 0.7f;

        public Action<int> alphaNumbersButtonEvent = delegate { };
        public Action rightClickMouseEvent = delegate { };
        public Action leftClickMouseEvent = delegate { };
        public Action<int> UpDownButtonKey = delegate { };
        public Action EnterButtonKey = delegate { };
        public Action EButtonKey = delegate { };
        public Action EscapeButtonKey = delegate { };

        private PlayerActionControls playerActionControls;

        private float _limitBetweenTouchButton = 0.15f;

        private float _timeLastLeftClick = 0;
        private float _timeLastRightClick = 0;
        private float _timeLastEnterClick = 0;
        private float _timeLastUpDownClick = 0;
        private float _timeLastEClick = 0;

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
            CheckTouchClickMouse();
            CheckTouchUpDownButtons();
            CheckTouchEnterButton();
            CheckTouchEButton();
            CheckTouchEscapeButton();

            _timeLastLeftClick += Time.deltaTime;
            _timeLastEnterClick += Time.deltaTime;
            _timeLastRightClick += Time.deltaTime;
            _timeLastUpDownClick += Time.deltaTime;
            _timeLastEClick += Time.deltaTime;
        }

        //inputs
        private bool OneAlphaInput() => playerActionControls.Player.Alpha1.ReadValue<float>() != 0.0f;
        private bool TwoAlphaInput() => playerActionControls.Player.Alpha2.ReadValue<float>() != 0.0f;
        private bool ThreeAlphaInput() => playerActionControls.Player.Alpha3.ReadValue<float>() != 0.0f;
        private bool RightClickMouseInput() => playerActionControls.Player.RightButton.ReadValue<float>() != 0.0f;
        private bool LeftClickMouseInput() => playerActionControls.Player.LeftButton.ReadValue<float>() != 0.0f;
        private bool UpButtonKeyInput() => playerActionControls.Player.UpButton.ReadValue<float>() != 0.0f;
        private bool DownButtonKeyInput() => playerActionControls.Player.DownButton.ReadValue<float>() != 0.0f;
        private bool EnterButtonKeyInput() => playerActionControls.Player.Enter.ReadValue<float>() != 0.0f;
        private bool EscapeButtonKeyInput() => playerActionControls.Player.Escape.ReadValue<float>() != 0.0f;
        private bool EButtonKeyInput() => playerActionControls.Player.E.ReadValue<float>() != 0.0f;
        public bool FButtonKeyInput() => playerActionControls.Player.InteractHouse.ReadValue<float>() != 0.0f;

        //checks
        private void CheckTouchButtonsInventory() {
            if (OneAlphaInput()) alphaNumbersButtonEvent.Invoke(0);
            else if(TwoAlphaInput()) alphaNumbersButtonEvent.Invoke(1);
            else if (ThreeAlphaInput()) alphaNumbersButtonEvent.Invoke(2);
        }

        private void CheckTouchClickMouse() {
            if (RightClickMouseInput() && _timeLastRightClick > _limitBetweenTouchButton) {
                _timeLastRightClick = 0;
                rightClickMouseEvent.Invoke();

            }
            else if (LeftClickMouseInput() && _timeLastLeftClick > _limitBetweenTouchButton)
            {
                _timeLastLeftClick = 0;
                leftClickMouseEvent.Invoke();
            }
        }

        private void CheckTouchUpDownButtons()
        {
            if (UpButtonKeyInput() && _timeLastUpDownClick > _limitBetweenTouchUpDownButton)
            {
                _timeLastUpDownClick = 0;
                UpDownButtonKey.Invoke(1);
            }
            else if (DownButtonKeyInput() && _timeLastUpDownClick > _limitBetweenTouchUpDownButton) {
                _timeLastUpDownClick = 0;
                UpDownButtonKey.Invoke(-1);
            }
        
        }

        private void CheckTouchEnterButton() {
            if (EnterButtonKeyInput() && _timeLastEnterClick > _maxTimeBetweenLeftClick) {
                _timeLastEnterClick = 0;
                EnterButtonKey.Invoke();

            }
        }

        private void CheckTouchEButton()
        {
            if (EButtonKeyInput() && _timeLastEClick > _maxTimeBetweenLeftClick)
            {
                _timeLastEClick = 0;
                EButtonKey.Invoke();
            }
        }

        private void CheckTouchEscapeButton()
        {
            if (EscapeButtonKeyInput())
            {
                EscapeButtonKey.Invoke();
            }
        }
    }
}

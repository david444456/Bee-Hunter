using BeeHunter.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeHunter.UI
{
    public class UiControlMoveStore : MonoBehaviour
    {
        public Action<int> TryBuyObjectEvent = delegate { };
        [SerializeField] RectTransform[] _rectTransformButtons;
        [SerializeField] Text[] _textButtonsPrice;
        [SerializeField] RectTransform _rectTransformSelectImage;

        PlayerBeeInput beeInput;

        [SerializeField] private int _actualIndex;

        void Start()
        {
            beeInput = FindObjectOfType<PlayerBeeInput>();
        }

        public void UpdateTextPriceButtons(int[] newPrice) {
            for (int i = 0; i<_textButtonsPrice.Length; i++) {
                _textButtonsPrice[i].text = newPrice[i].ToString();
            }
        }

        public void ChangeActiveStoreUIMovement(bool newValue) {
            if (newValue)
            {
                beeInput.UpDownButtonKey += MoveRectTransformSelect;
                beeInput.EnterButtonKey += TryButItem;
            }
            else
            {
                beeInput.UpDownButtonKey -= MoveRectTransformSelect;
                beeInput.EnterButtonKey -= TryButItem;
            }
        }

        private void MoveRectTransformSelect(int value) {
            value *= -1;
            print(_actualIndex);
            if (InRange(value + _actualIndex)) {
                _actualIndex += value;
                _rectTransformSelectImage.anchoredPosition = _rectTransformButtons[_actualIndex].anchoredPosition;
            }
        }

        private void TryButItem() {
            TryBuyObjectEvent.Invoke(_actualIndex);
        }

        private bool InRange(int value) => value >= 0 && value < _rectTransformButtons.Length;

    }
}

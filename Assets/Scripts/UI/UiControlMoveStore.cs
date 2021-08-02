using BeeHunter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeHunter.UI
{
    public class UiControlMoveStore : MonoBehaviour
    {
        [SerializeField] RectTransform[] _rectTransformButtons;
        [SerializeField] RectTransform _rectTransformSelectImage;

        PlayerBeeInput beeInput;

        private int _actualIndex;

        void Start()
        {
            beeInput = FindObjectOfType<PlayerBeeInput>();
        }

        public void ChangeActiveStoreUIMovement(bool newValue) {
            if(newValue)
                beeInput.UpDownButtonKey += MoveRectTransformSelect;
            else
                beeInput.UpDownButtonKey -= MoveRectTransformSelect;
        }

        private void MoveRectTransformSelect(int value) {
            value *= -1;
            if (InRange(value + _actualIndex)) {
                _actualIndex += value;
                _rectTransformSelectImage.anchoredPosition = _rectTransformButtons[_actualIndex].anchoredPosition;
            }
        }

        private bool InRange(int value) => value >= 0 && value < _rectTransformButtons.Length;

    }
}

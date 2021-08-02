using BeeHunter.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeeHunter.Slots
{
    public class PlotStore : MonoBehaviour
    {
        [SerializeField] GameObject _GOStore;

        UiControlMoveStore uiControlMove;

        private void Start()
        {
            uiControlMove = GetComponent<UiControlMoveStore>();
        }

        public void ChangeStateActiveStore(bool newValue) {
            _GOStore.SetActive(newValue);
            uiControlMove.ChangeActiveStoreUIMovement(newValue);
        }
    }
}

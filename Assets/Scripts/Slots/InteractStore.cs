using BeeHunter.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Slots
{
    public class InteractStore : MonoBehaviour
    {

        PlayerUI playerUI;

        private bool _isActiveUIInteract;
        private PlotStore _lastPlotStore;

        void Start()
        {
            playerUI = GetComponent<PlayerUI>();

            GetComponent<PlayerBeeInput>().EButtonKey += ActiveInteractWithStore;
            GetComponent<PlayerBeeInput>().EscapeButtonKey += DesactiveInteractWithStore;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Store")
            {
                ChangeStateStorePlot(other, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Store")
            {
                ChangeStateStorePlot(null, false);
                other.GetComponent<PlotStore>().ChangeStateActiveStore(false);
            }
        }

        private void ChangeStateStorePlot(Collider other, bool newState)
        {
            if (other != null) _lastPlotStore = other.GetComponent<PlotStore>();
            else _lastPlotStore = null;


            playerUI.ChangeStateTouchStoreUI(newState);
            _isActiveUIInteract = newState;
        }

        private void ActiveInteractWithStore() {
            if(_lastPlotStore != null) _lastPlotStore.ChangeStateActiveStore(true);
        }

        private void DesactiveInteractWithStore()
        {
            if (_lastPlotStore != null) _lastPlotStore.ChangeStateActiveStore(false);
        }

    }
}

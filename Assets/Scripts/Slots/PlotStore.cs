using BeeHunter.Player;
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

        [Header("Store logic")]
        [SerializeField] GameObject[] _PrefabsBuy;
        [SerializeField] int[] _pricePrefabsBuy;
        [SerializeField] GameObject _lastGOSpawn;
        [SerializeField] Transform _TFPositionToSpawnNewObject;
        [SerializeField] ParticleSystem _effectSpawn;

        UiControlMoveStore uiControlMove;
        PlayerCoin playerCoin;

        private void Start()
        {
            uiControlMove = GetComponent<UiControlMoveStore>();
            playerCoin = FindObjectOfType<PlayerCoin>();

            //events
            uiControlMove.TryBuyObjectEvent += TryBuyObjectInThisPlot;

            //ui
            uiControlMove.UpdateTextPriceButtons(_pricePrefabsBuy);
        }

        public void ChangeStateActiveStore(bool newValue) {
            _GOStore.SetActive(newValue);
            uiControlMove.ChangeActiveStoreUIMovement(newValue);
        }

        public void TryBuyObjectInThisPlot(int index) {
            //ui desactive
            ChangeStateActiveStore(false);

            //if there are other object prefab
            if (_lastGOSpawn != null) return;

            //destroy button
            if (index == _PrefabsBuy.Length)
            {
                if (_lastGOSpawn != null) Destroy(_lastGOSpawn);
                return;
            }
            else if (!InRange(index)) return; //range index

            //other buttons
            //coins, buy item
            if (playerCoin.GetCanBuyItemWithPrice(_pricePrefabsBuy[index]))
            {
                playerCoin.SetAugmentCoin(-_pricePrefabsBuy[index]);

                //effect
                _effectSpawn.Play();

                //spawn
                _lastGOSpawn = Instantiate(_PrefabsBuy[index],
                                            _TFPositionToSpawnNewObject.position + _PrefabsBuy[index].transform.position,
                                            _PrefabsBuy[index].transform.rotation, _TFPositionToSpawnNewObject);
            }
        }

        private bool InRange(int value) => value >= 0 && value < _PrefabsBuy.Length;
    }
}

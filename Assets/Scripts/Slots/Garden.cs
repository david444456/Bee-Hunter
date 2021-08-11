using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Slots
{
    public class Garden : MonoBehaviour
    {
        [SerializeField] Transform[] transformPositionByNewFlowers;

        ControlFlower _lastControlFlower;
        FlowerItem _lastFlowerItem;
        int _ticksActualFlower = 0;

        void Start()
        {

        }

        public void AddNewItemFlowerToGarden(GameObject GOFlower) {
            if (GOFlower.tag != "Flower") return;
            if (_ticksActualFlower > 0) return;


            print("New object flower: " + GOFlower.name);

            //info flower
            _lastControlFlower = GOFlower.GetComponentInParent<ControlFlower>();
            _lastFlowerItem = _lastControlFlower.GetActualFlowerItem();
            _ticksActualFlower = _lastFlowerItem.GetCountTicksByGarden();

            //works with garden
            StartSpawnFlower();

            //destroy first flower
            Destroy(GOFlower);
        }

        private void StartSpawnFlower() {
            for (int i = 0; i < transformPositionByNewFlowers.Length; i++) {
                GameObject newGameObjectFlower = Instantiate(_lastFlowerItem.GetActualGOPrefab(), 
                    transformPositionByNewFlowers[i].transform.position,
                    Quaternion.identity, transform);

                //subscribe event when this destroy

            }

            _ticksActualFlower--;
        }
    }
}

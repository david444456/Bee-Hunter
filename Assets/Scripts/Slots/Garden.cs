using BeeHunter.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Slots
{
    /// <summary>
    /// When this script is active, and receive a flower, it start to spawn "seed flowers", that increment by time.
    /// Also, it control the time to increment size from the flower, and the time between spawn the flowers.
    /// How it works, when receive a flower, a seed flower is started produced like the flower that received,
    /// and generated in the transforms position. When is finished, wait for the player grab the flowers, and start again.
    /// </summary>
    public class Garden : MonoBehaviour
    {
        [SerializeField] Transform[] transformPositionByNewFlowers;
        [SerializeField] float _maxTimeBetweenProduceFlowers = 5f;
        [SerializeField] float _minTimeBetweenProduceFlowers = 3;

        ControlFlower _lastControlFlower;
        FlowerItem _lastFlowerItem;
        int _ticksActualFlower = 0;

        int _flowersInTheGarden = 0;
        int _actualCountFlowersPickUp = 0;

        bool _canProduceMoreFlowers = false;
        float _maxTimeBetweenProduceFlowersChangeValue = 6;
        float _actualTimeLastProduceFlowers = 0;

        void Start()
        {

        }

        private void Update()
        {
            if (_canProduceMoreFlowers) {
                _actualTimeLastProduceFlowers += Time.deltaTime;
                if (_maxTimeBetweenProduceFlowersChangeValue < _actualTimeLastProduceFlowers) {
                    StartSpawnFlower();
                }
            }
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
            ProduceNewSpawnFlower();

            //destroy first flower
            Destroy(GOFlower);
        }

        private void StartSpawnFlower() {
            if (_ticksActualFlower <= 0) return;

            _canProduceMoreFlowers = false;

            //count how many flowers there are in the garden
            _flowersInTheGarden = transformPositionByNewFlowers.Length;
            _actualCountFlowersPickUp = 0;

            //spawn
            for (int i = 0; i < transformPositionByNewFlowers.Length; i++) {
                GameObject newGameObjectFlower = Instantiate(_lastFlowerItem.GetActualPrefabSeedFlower(), 
                    transformPositionByNewFlowers[i].transform.position,
                    Quaternion.identity, transform);

                //seed 
                newGameObjectFlower.GetComponent<SeedFlower>().SetTimeSpawnFlower(_lastFlowerItem.GetRandomTimeSeedToSpawnFlower());

                //subscribe event when this destroy
                newGameObjectFlower.GetComponent<ControlItemObject>().DesactiveObjectEvent += PickUpNewFlower;
            }

            _ticksActualFlower--;
        }

        private void PickUpNewFlower() {
            _actualCountFlowersPickUp++;
            if (_actualCountFlowersPickUp >= _flowersInTheGarden)
            {
                ProduceNewSpawnFlower();
            }
        }

        private void ProduceNewSpawnFlower()
        {
            //for produce new spawn flower, it wait time and produce them
            _canProduceMoreFlowers = true;
            _actualTimeLastProduceFlowers = 0;
            _maxTimeBetweenProduceFlowersChangeValue = GetRandomTimeForSpawnFlower();
        }

        private float GetRandomTimeForSpawnFlower() => Random.Range(_minTimeBetweenProduceFlowers, _maxTimeBetweenProduceFlowers);
    }
}

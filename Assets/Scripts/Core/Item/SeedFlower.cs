using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    /// <summary>
    /// control the new flower item, the seed flower, and control the time to increment size (growing up)
    /// and the time to produce it. When the grow is done, the player can touch the flower and interact with it.
    /// </summary>
    public class SeedFlower : MonoBehaviour
    {
        [SerializeField] private int _numberToDivideTheCountSpawn = 4;

        private int _countToIncrementSizeFlower = 0;

        private float _countTimeToSpawnFlower = 0;
        private float _actualTimeToSpawnFlower = 0;

        private float _sizeToIncrementObject = 0;

        Collider _colliderDetectPlayer;

        void Start()
        {
            _colliderDetectPlayer = GetComponent<Collider>();
            _colliderDetectPlayer.enabled = false;

            if (_numberToDivideTheCountSpawn == 0) Debug.LogError("You can't divide by 0!");
            else
            {
                transform.localScale = transform.localScale / _numberToDivideTheCountSpawn;
                _sizeToIncrementObject = transform.localScale.x;
            }
        }

        private void Update()
        {
            _actualTimeToSpawnFlower += Time.deltaTime;

            if (_actualTimeToSpawnFlower > _countTimeToSpawnFlower) {
                _countToIncrementSizeFlower++;
                _actualTimeToSpawnFlower = 0;

                //size
                transform.localScale += new Vector3(_sizeToIncrementObject, _sizeToIncrementObject, _sizeToIncrementObject);

                //count spawn flower
                if (_countToIncrementSizeFlower >= _numberToDivideTheCountSpawn)
                {
                    print("I already grew up");
                    _colliderDetectPlayer.enabled = true;
                    this.enabled = false;
                }
            }
        }

        public void SetTimeSpawnFlower(float time) => _countTimeToSpawnFlower = time/ _numberToDivideTheCountSpawn;

    }
}

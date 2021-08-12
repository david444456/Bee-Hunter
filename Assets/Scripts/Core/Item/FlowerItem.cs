using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    [CreateAssetMenu(fileName = "Flower 1", menuName = "Items/NewFlower", order = 0)]
    public class FlowerItem : Item
    {
        [Header("Flower")]
        [SerializeField] GameObject _prefabSeedFlower;
        [SerializeField] int _maxUseFlower = 3;
        [SerializeField] int _maxTicksForGarden = 4;
        [SerializeField] float _minTimeToRaiseSeedToFlower = 5;
        [SerializeField] float _maxTimeToRaiseSeedToFlower = 20;

        public override string GetNameItem() => this.name;

        public int GetMaxUsesFlower() => _maxUseFlower;

        public int GetCountTicksByGarden() => _maxTicksForGarden;

        public GameObject GetActualPrefabSeedFlower() => _prefabSeedFlower;

        public float GetRandomTimeSeedToSpawnFlower() => Random.Range(_minTimeToRaiseSeedToFlower, _maxTimeToRaiseSeedToFlower);

    }
}

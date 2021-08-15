using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ControlSpawnObject : MonoBehaviour
    {
        [SerializeField] TypeSpawnObject[] allTypesSpawn;
        [SerializeField] int _maxCountSpawnObjects = 12;

        Dictionary<TypeSpawnObject, int> _countSpawnObjects = new Dictionary<TypeSpawnObject, int>();
        SpawnObjectArea[] spawnObject;

        void Start()
        {
            AddTypeSpawnToDictionary();

            spawnObject = FindObjectsOfType<SpawnObjectArea>();

            //eliminated
            StartWithSpawnInDifferentsSpawnsObjects();
        }

        private void StartWithSpawnInDifferentsSpawnsObjects() {
            for (int i = 0; i < spawnObject.Length; i++) {
                spawnObject[i].StartDaySpawnObjects();
            }
        }

        public bool CanSpawnNewObjectByType(TypeSpawnObject typeSpawnObject) {
            if (_countSpawnObjects[typeSpawnObject] < _maxCountSpawnObjects)
            {
                _countSpawnObjects[typeSpawnObject]++;
                return true;
            }
            return false;
        }

        private void AddTypeSpawnToDictionary()
        {
            for (int i = 0; i < allTypesSpawn.Length; i++) {
                _countSpawnObjects.Add(allTypesSpawn[i], 0);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class SpawnObjectArea : MonoBehaviour
    {
        [SerializeField] TypeSpawnObject _actualTypeSpawn;
        [SerializeField] int _indexItemToSpawn;
        [SerializeField] Transform[] _limitsToSpawnObject;

        [Header("Values")]
        [SerializeField] int _maxCountSpawnObjects = 3;
        [SerializeField] float _maxTimeBetweenGenerateNewObject = 0.2f;

        ControlSpawnObject _controlSpawnObject;
        IObjectFactory _objectFactory;
        int _actualCountObjectThisSpawn = 0; 

        void Start()
        {
            _controlSpawnObject = FindObjectOfType<ControlSpawnObject>();
            _objectFactory = FindObjectOfType<ObjectItemFactory>();
        }

        /// <summary>
        /// When start the day, this method is called by a event.
        /// </summary>
        public void StartDaySpawnObjects() {
            _actualCountObjectThisSpawn = 0;
            StartCoroutine(SpawnObject());
        }

        private IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(RandomTimeToSpawn());

            if (CheckCanSpawnWithLimitItems())
            {
                InstantiateNewObject();
                _actualCountObjectThisSpawn++;
                StartCoroutine(SpawnObject());
            }
        }

        private float RandomTimeToSpawn() => Random.Range(0, _maxTimeBetweenGenerateNewObject);

        private bool CheckCanSpawnWithLimitItems() => _controlSpawnObject.CanSpawnNewObjectByTypeAndAugmentCount(_actualTypeSpawn) && _actualCountObjectThisSpawn < _maxCountSpawnObjects;

        private void InstantiateNewObject()
        {
            GameObject prefabForSpawn = _objectFactory.GetObjectToSpawnById(_indexItemToSpawn);
            Instantiate(prefabForSpawn, GetPositionToSpawnRandom(), Quaternion.identity, this.transform);
        }

        private Vector3 GetPositionToSpawnRandom() {
            //get values
            float minX = _limitsToSpawnObject[0].position.x;
            float minZ = _limitsToSpawnObject[0].position.z;
            float maxX = _limitsToSpawnObject[0].position.x;
            float maxZ = _limitsToSpawnObject[0].position.z;

            for (int i = 1; i < _limitsToSpawnObject.Length - 1; i++)
            {
                minX = Mathf.Min(minX, _limitsToSpawnObject[i + 1].position.x);
                minZ = Mathf.Min(minZ, _limitsToSpawnObject[i + 1].position.z);
                maxX = Mathf.Max(maxX, _limitsToSpawnObject[i + 1].position.x);
                maxZ = Mathf.Max(maxZ, _limitsToSpawnObject[i + 1].position.z);
            }

            //make the direction
            Vector3 newPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), 0, UnityEngine.Random.Range(minZ, maxZ));
            return newPosition;
        }
    }
}

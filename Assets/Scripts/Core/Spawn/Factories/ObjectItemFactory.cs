using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public class ObjectItemFactory : MonoBehaviour, IObjectFactory
    {
        [SerializeField] List<Item> _objectItems;
        Dictionary<int, Item> _idItems;

        private void Awake()
        {
            _idItems = new Dictionary<int, Item>();
            foreach (Item objectCompareItem in _objectItems)
            {
                _idItems.Add(objectCompareItem.GetActualIndexItem(), objectCompareItem);
                print(objectCompareItem.GetActualIndexItem());
            }
        }

        public GameObject GetObjectToSpawnById(int id)
        {
            if (!_idItems.TryGetValue(id, out var objectActualItem)) {
                throw new Exception($"Error! this id doesnt exist, ${id}");
            }
            
            return objectActualItem.GetActualGOPrefab();
        }
    }
}

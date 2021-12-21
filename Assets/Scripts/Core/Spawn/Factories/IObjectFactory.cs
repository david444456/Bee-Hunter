using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core
{
    public interface IObjectFactory
    {
        public GameObject GetObjectToSpawnById(int id);
    }
}
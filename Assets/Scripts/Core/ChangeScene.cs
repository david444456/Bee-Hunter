using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeeHunter.Core
{
    public class ChangeScene : MonoBehaviour
    {


        void Start()
        {

        }

        public void ChangeSceneTo(int index) {
            SceneManager.LoadScene(index);
        }
    }
}

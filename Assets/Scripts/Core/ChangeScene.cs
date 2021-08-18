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
            //SceneManager.LoadScene(index);
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene() {
            SceneManager.LoadScene("Prot", LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Prot"));
        }
    }
}

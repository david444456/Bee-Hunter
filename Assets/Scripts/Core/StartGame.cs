using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    [SerializeField] UnityEvent _eventStartGame;

    // Start is called before the first frame update
    void Start()
    {
        _eventStartGame.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

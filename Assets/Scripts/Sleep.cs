using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Quaternion rotation = Quaternion.Euler(0, 0, 0);
    [SerializeField] private GameObject houseUI;
    
    public void Sleeping()
    {
        transform.rotation = rotation;
        houseUI.SetActive(false);
    }
}

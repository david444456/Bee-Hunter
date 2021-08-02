using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleDayNight : MonoBehaviour
{
    [SerializeField] private float lightRotation = 1;

    void Update()
    {
        transform.Rotate(lightRotation * Time.deltaTime,0,0);
    }
}

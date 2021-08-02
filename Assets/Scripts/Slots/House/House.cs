using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private GameObject pressF;
    [SerializeField] private GameObject uiHouse;
    void Update()
    {
        RaycastHit rayHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit, 3, doorLayer))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.name == "Door")
                {
                    pressF.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        uiHouse.SetActive(true);
                    }
                }
            }
            if (rayHit.collider == null)
            {
                pressF.SetActive(false);
            }
        }
    }
}

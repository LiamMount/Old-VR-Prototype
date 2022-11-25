using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBillboard : MonoBehaviour
{
    public Transform cam;

    public void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

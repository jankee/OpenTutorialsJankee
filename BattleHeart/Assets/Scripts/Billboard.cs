using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
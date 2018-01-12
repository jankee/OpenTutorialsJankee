using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private float xMin, xMax, yMin, yMax;


    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    private void SetLimite(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;

    }
}

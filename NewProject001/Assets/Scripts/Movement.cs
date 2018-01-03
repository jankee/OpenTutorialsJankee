using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform tr;

    public float rSpeed;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool LRot = Input.GetKey(KeyCode.Z);
        bool RRot = Input.GetKey(KeyCode.X);

        if (LRot && !RRot)
        {
            tr.Rotate(Vector3.forward * rSpeed * Time.deltaTime);
        }
        else if (!LRot && RRot)
        {
            tr.Rotate(Vector3.forward * -rSpeed * Time.deltaTime);
        }
    }
}

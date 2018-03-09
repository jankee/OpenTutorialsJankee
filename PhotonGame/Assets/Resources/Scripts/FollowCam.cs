using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform target;

    public float smooth;

    public Vector3 offset;

    // Use this for initialization
    public void Init(Transform target)
    {
        this.target = target;

        transform.position = Vector3.zero;
        transform.position = this.target.position + offset;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooth * Time.deltaTime);

        //Vector3.MoveTowards()
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    private void LateUpdate()
    {
        print("Effect");
        transform.LookAt(Camera.main.transform.position);
    }
}
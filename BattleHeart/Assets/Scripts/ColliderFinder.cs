using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFinder : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Find good");

        if (other.tag == "Player")
        {
            print("Find");
        }
    }
}
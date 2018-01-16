using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Ray ray;

    public void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        //worldPoint.y = 0;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        print("World Point : " + worldPoint + "Mouse Pos : " + mousePos);

    }

    //public void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        print("Mounse : " + Input.mousePosition);

    //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        print("Ray : " + ray);
    //    }
    //}




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color32(145, 245, 138, 255);
        Gizmos.DrawLine(ray.origin, ray.direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Camera _mainCamera = null;
    private bool _mouseState;
    private GameObject target;
    private Vector3 mousePos;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;

            target = GetClickedObject(out hitInfo);

            if (target != null)
            {
                _mouseState = true;
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                target.transform.position = new Vector3(mousePos.x, mousePos.y, target.transform.position.z);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
        }

        if (_mouseState)
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            target.transform.position = new Vector3(mousePos.x, mousePos.y, target.transform.position.z);
        }
    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}

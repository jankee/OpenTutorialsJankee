using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class DragPrefab : MonoBehaviour
{

    Vector3 startPos;

    Vector3 endPos;

    [SerializeField]
    Player player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDragStart(Gesture gesture)
    {
        print( gesture.pickedObject.transform.position);

        startPos = gesture.pickedObject.transform.position;
    }

    public void OnDragEnd(Gesture gesture)
    {
        endPos = gesture.pickedObject.transform.position;

        GameManager.MyInstance.MyPlayer.Move(startPos, endPos);
    }
}

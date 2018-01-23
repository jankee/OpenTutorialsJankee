using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class TouchMe : MonoBehaviour
{

    private TextMesh textMesh;
    private Color startColor;

    // Subscribe to events
    void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
        EasyTouch.On_TouchDown += On_TouchDown;
        EasyTouch.On_TouchUp += On_TouchUp;
    }

    void OnDisable()
    {
        UnsubscribeEvent();
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
        EasyTouch.On_TouchDown -= On_TouchDown;
        EasyTouch.On_TouchUp -= On_TouchUp;
    }

    void Start()
    {
        startColor = gameObject.GetComponentInChildren<Renderer>().material.color;
    }

    // At the touch beginning 
    private void On_TouchStart(Gesture gesture)
    {
        if (gesture.pickedObject == gameObject)
        {
            RandomColor();
        }
    }

    // During the touch is down
    private void On_TouchDown(Gesture gesture)
    {

        // Verification that the action on the object
        if (gesture.pickedObject == gameObject)
        {
            print(gesture.pickedObject.name + " : " + gesture.pickedObject.transform.position);
        }

    }

    // At the touch end
    private void On_TouchUp(Gesture gesture)
    {

        // Verification that the action on the object
        if (gesture.pickedObject == gameObject)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = startColor;
            
        }
    }

    private void RandomColor()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}

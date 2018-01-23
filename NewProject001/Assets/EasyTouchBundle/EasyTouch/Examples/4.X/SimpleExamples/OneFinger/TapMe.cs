using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class TapMe : MonoBehaviour
{

    // Subscribe to events
    void OnEnable()
    {
        EasyTouch.On_SimpleTap += On_SimpleTap;
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
        EasyTouch.On_SimpleTap -= On_SimpleTap;
    }

    // Simple tap
    private void On_SimpleTap(Gesture gesture)
    {
        // Verification that the action on the object
        if (gesture.pickedObject == gameObject)
        {
            print(gesture.pickedObject.name + " : " + gesture.pickedObject.transform.position);

        }
    }
}

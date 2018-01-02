using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    RoomWithDoor livingRoom;
    RoomWithDoor kitchen;

    Room diningRoom;

    OutsideWithDoor frontYard;
    OutsideWithDoor backYard;

    OutSide garden;

    private void Start()
    {
        livingRoom = new RoomWithDoor("LivingRoom");
        kitchen = new RoomWithDoor("Kitchen");

        diningRoom = new Room("DiningRoom");

        frontYard = new OutsideWithDoor("FrontYard");
        backYard = new OutsideWithDoor("BackYard");

        garden = new OutSide("Garden");

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(livingRoom.RoomName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Text description;

    [SerializeField]
    private Dropdown dropdownButton;

    [SerializeField]
    private Button goThroughTheDoor;

    private string dropdownText;

    private List<string> dropdownButtonList;

    private Location currentLocatin;

    private List<string> exits;

    RoomWithDoor livingRoom;
    RoomWithDoor kitchen;

    Room diningRoom;

    OutsideWithDoor frontYard;
    OutsideWithDoor backYard;

    OutSide garden;

    private void Start()
    {
        livingRoom = new RoomWithDoor("LivingRoom", "antique carpat", "an oak door with a brass knob");
        kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "a screen door");

        diningRoom = new Room("DiningRoom", "a crystal chandelier");

        frontYard = new OutsideWithDoor("FrontYard", false, "an oak door with a brass knob");
        backYard = new OutsideWithDoor("BackYard", true, "a screen door");

        garden = new OutSide("Garden", false);

        livingRoom.Exits = new Location[] { diningRoom };
        diningRoom.Exits = new Location[] { livingRoom, kitchen };
        kitchen.Exits = new Location[] { diningRoom };

        frontYard.Exits = new Location[] { garden };
        backYard.Exits = new Location[] { garden };
        garden.Exits = new Location[] { backYard, frontYard };

        livingRoom.DoorLocation = "frontYard";
        frontYard.DoorLocation = "livingRoom";

        kitchen.DoorLocation = "backYard";
        backYard.DoorLocation = "kitchen";

        //dropdownButtonArray = new string[] { "diningRoom", "livingRoom", "kitchen", "frontYard", "backYard", "garden" };

        dropdownButtonList = new List<string>() { "diningRoom", "livingRoom", "kitchen", "frontYard", "backYard", "garden" };

        dropdownButton.AddOptions(dropdownButtonList);

        exits = new List<string>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(livingRoom.RoomName);

            //livingRoom.Decoration = ""

            print(livingRoom.DoorLocation);

            for (int i = 0; i < diningRoom.Exits.Length; i++)
            {
                print(diningRoom.Exits[i].RoomName);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveToNewLocation(frontYard);

        }
    }

    public void GetIndex(int index)
    {
        if (exits != null)
        {
            print(exits[index]);

            dropdownText = exits[index];
        }
        
    }

    public void GoHere()
    {
        if (dropdownText != null)
        {
            description.text = dropdownText;

            dropdownText = null;
        }
        
    }

    public void MoveToNewLocation(Location newLocation)
    {
        currentLocatin = newLocation;

        //exits리스트 초기화
        //exits = null;
        exits.Clear();

        dropdownButton.ClearOptions();

        for (int i = 0; i < currentLocatin.Exits.Length; i++)
        {

            exits.Add(currentLocatin.Exits[i].RoomName);
        }

        if (currentLocatin is RoomWithDoor)
        {
            RoomWithDoor tRoom = currentLocatin as RoomWithDoor;

            exits.Add(tRoom.DoorLocation);

            print("RoomWithDoor");
        }
        else if (currentLocatin is OutsideWithDoor)
        {
            OutsideWithDoor tRoom = currentLocatin as OutsideWithDoor;

            exits.Add(tRoom.DoorLocation);

            print("OutsideWithDoor");
        }

        dropdownButton.AddOptions(exits);

        if (currentLocatin is IHasExteriorDoor)
        {
            goThroughTheDoor.gameObject.SetActive(false);
        }
        else
        {
            goThroughTheDoor.gameObject.SetActive(true);

        }

        //print(exits);
    }
}

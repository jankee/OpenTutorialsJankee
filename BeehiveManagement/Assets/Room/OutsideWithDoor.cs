using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWithDoor : OutSide, IHasExteriorDoor
{
    public OutsideWithDoor(string name):base(name)
    {

    }

    //DoorLocation 속성이 들어갈 자리
    //읽기 전용 DoorDescription 속성이 들어갈 자리
    public string doorDescription
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string doorLocation
    {
        get
        {
            throw new NotImplementedException();
        }
    }
}

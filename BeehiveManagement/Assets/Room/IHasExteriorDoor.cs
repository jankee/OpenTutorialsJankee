using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasExteriorDoor
{
    string DoorDescription { get; }

    string DoorLocation { get; set; }
}

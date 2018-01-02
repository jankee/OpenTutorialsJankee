using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasExteriorDoor
{
    string doorDescription { get; }

    string doorLocation { get; }
}

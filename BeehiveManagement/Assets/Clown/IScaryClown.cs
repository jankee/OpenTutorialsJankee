using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScaryClown : IClown
{
    string ScaryThingIHave { get; }

    void ScaryLittleChildren();
}

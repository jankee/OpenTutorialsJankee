using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private bool bMove = false;

    public bool MyMove
    {
        get
        {
            return bMove;
        }
    }

    public Enemy MyTarget { get; set; }

    public Coroutine MyMoveRoutine { get; set; }

    // Use this for initialization
    protected override void Start()
    {
        bMove = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
    }

    public override IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        return base.MoveRoutine(moveEnd, rotate);
    }
}
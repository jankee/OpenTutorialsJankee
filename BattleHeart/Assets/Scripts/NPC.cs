using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private Player[] players;

    private Coroutine moveRoutine;

    protected override void Start()
    {
        FindEnemy();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        //FindEnemy();
    }

    public virtual void DeSelect()
    {
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public virtual void FindEnemy()
    {
        players = FindObjectsOfType<Player>();

        Vector3 tmpPos = this.transform.position;

        Vector3 startPos = new Vector3(tmpPos.x, 0, tmpPos.z);

        Vector3 tmpEndPos = players[0].transform.position;

        Vector3 endPos = new Vector3(tmpEndPos.x, 0, tmpEndPos.z);

        Vector3 rotData = endPos - startPos;

        float rota = (Mathf.Atan2(rotData.z, rotData.x) * Mathf.Rad2Deg) + 180f;

        moveRoutine = StartCoroutine(MoveRoutine(endPos, rota));

        //if (Vector3.Distance(endPos, startPos) > 3f)
        //{
        //    print("move" + Vector3.Distance(endPos, startPos));

        //}
        //else
        //{
        //    print("Find");
        //    StopCoroutine(moveRoutine);

        //    //StopAllCoroutines();
        //}
    }
}
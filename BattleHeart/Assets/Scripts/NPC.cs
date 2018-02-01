using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private Player[] players;

    private Coroutine moveRoutine = null;

    private bool isAttack = false;

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
        //멈추었다면
        if (IsMoving == false)
        {
            //공격거리에 Player가 있는지 확인 한다
            //없으면 다시 움직인다

            if (Vector3.Distance(endPos, startPos) > 2.5f)
            {
                isAttack = true;

                Attack();
            }
            else
            {
                FindEnemy();
            }
        }
    }

    public IEnumerator Attack()
    {
        //공격
        print("공격");

        while (true)
        {
        }
    }

    public virtual void DeSelect()
    {
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void DistanceToEnemy()
    {
        Vector3 tmpPos = this.transform.position;

        Vector3 startPos = new Vector3(tmpPos.x, 0, tmpPos.z);

        Vector3 tmpEndPos = players[0].transform.position;

        Vector3 endPos = new Vector3(tmpEndPos.x, 0, tmpEndPos.z);
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
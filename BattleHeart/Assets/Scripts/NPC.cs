using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    private Player[] players;

    private Coroutine moveRoutine = null;

    private Coroutine attackRoutine = null;

    private bool isAttack = false;

    private Vector3 startPos;

    private Vector3 endPos;

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

                if (attackRoutine != null)
                {
                    print("국제 : " + isAttack);
                    StopCoroutine(attackRoutine);
                }

                attackRoutine = StartCoroutine(Attack());
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

        float time = 0f;

        while (time < 1f)
        {
            print("공격 : " + isAttack);

            yield return null;
            time += Time.deltaTime;
        }
        time = 0f;

        isAttack = false;
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

        startPos = new Vector3(tmpPos.x, 0, tmpPos.z);

        Vector3 tmpEndPos = players[0].transform.position;

        endPos = new Vector3(tmpEndPos.x, 0, tmpEndPos.z);
    }

    public virtual void FindEnemy()
    {
        players = FindObjectsOfType<Player>();

        //적의 위치를 찾는다
        DistanceToEnemy();

        Vector3 rotData = endPos - startPos;

        float rotateTo = (Mathf.Atan2(rotData.z, rotData.x) * Mathf.Rad2Deg) + 180f;

        moveRoutine = StartCoroutine(MoveRoutine(endPos, rotateTo));
    }
}
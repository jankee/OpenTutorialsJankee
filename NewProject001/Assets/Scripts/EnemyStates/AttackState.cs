using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;

    private Coroutine attackRountine;

    private float attackCoolTime = 3f;

    private float extraRange = 0.1f;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        Debug.Log("Attack");

        if (parent.MyAttackTime >= attackCoolTime && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;

            attackRountine = parent.StartCoroutine(Attack());
        }

        if (parent.MyTarget != null)
        {
            float distance = Vector3.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance >= parent.MyAttackRange + extraRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;

        parent.MyAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);

        parent.IsAttacking = false;
    }
}

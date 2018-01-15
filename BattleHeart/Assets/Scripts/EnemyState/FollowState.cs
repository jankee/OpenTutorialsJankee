using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        parent.Direction = Vector3.zero;
    }

    public void Update()
    {
        Debug.Log("Follow");

        if (parent.MyTarget != null)
        {
            parent.Direction = (parent.MyTarget.transform.position - parent.transform.position).normalized;

            parent.transform.position = Vector3.MoveTowards(parent.transform.position, parent.MyTarget.position, parent.Speed * Time.deltaTime);

            float distant = Vector3.Distance(parent.MyTarget.position, parent.transform.position);

            if (distant <= parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
        if (!parent.InRange)
        {
            Debug.Log("InRange out");
            parent.ChangeState(new EvadeState());
        }
    }

    
}

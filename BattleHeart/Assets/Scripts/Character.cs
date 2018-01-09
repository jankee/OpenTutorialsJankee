﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    protected Animator myAnimator;

    private Rigidbody myRigibody;

    protected Vector3 direction;

    protected Coroutine attackRoutine;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.z != 0;
        }
    }

    protected bool IsAttacking = false;

    // Use this for initialization
    protected virtual void Start()
    {
        myRigibody = this.GetComponent<Rigidbody>();
        myAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigibody.velocity = direction.normalized * speed;
    }

    public void HandleLayer()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("X", direction.x);
            myAnimator.SetFloat("Z", direction.z);

            StopAttack();
        }
        else if (IsAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }       
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        IsAttacking = false;

        myAnimator.SetBool("Attack", IsAttacking);

        if (attackRoutine != null)
        {
            //자식의 코루틴을 접근하기 위해서
            StopCoroutine(attackRoutine);

            print("Attack Stoped");
        }
    }
}

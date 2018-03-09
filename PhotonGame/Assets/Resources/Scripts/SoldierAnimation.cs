using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : MonoBehaviour
{
    private SoldierStat _state;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _state = GetComponent<SoldierStat>();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlayAnim(SoldierStat.STATE state)
    {
        _state.state = state;

        switch (state)
        {
            case SoldierStat.STATE.IDLE:
                animator.SetBool("MOVE", false);
                break;

            case SoldierStat.STATE.MOVE:
                animator.SetBool("MOVE", true);
                break;

            case SoldierStat.STATE.ATTACK:
                break;

            case SoldierStat.STATE.DEATH:
                animator.SetTrigger("DEATH");
                break;
        }
    }
}
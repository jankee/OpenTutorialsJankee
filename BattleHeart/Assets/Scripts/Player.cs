using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    [SerializeField]
    private GameObject[] spellPrefabs;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform exitPoint;

    //[SerializeField]
    //private float healthValue;

    private float initHealth = 100;

    private float initMana = 50;

    private Transform target;

    private int exitIndex;

    // Use this for initialization
    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);

        mana.Initialize(initMana, initMana);

        target = GameObject.Find("Enemy").transform;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        InLineOfSight();

        base.Update();
    }

    public void GetInput()
    {
        direction = Vector3.zero;

        //THIS IS USED FOR DEBUGGING ONLY
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }
        //--------------------------------

        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 1;
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 3;
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Block();

            if (!IsAttacking && !IsMoving)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }

    }

    private IEnumerator Attack()
    {

        IsAttacking = true;

        myAnimator.SetBool("Attack", true);

        yield return new WaitForSeconds(0.8f);
        print("Attackt Done");

        CastSpell();

        StopAttack();
    }

    public void CastSpell()
    {
        Instantiate(spellPrefabs[0], exitPoint.position, Quaternion.identity);
    }

    private bool InLineOfSight()
    {
        Vector3 targetDirection = (target.position - exitPoint.position).normalized;

        Debug.DrawLine(exitPoint.position, targetDirection, Color.red);

        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}

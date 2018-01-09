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

    public Transform MyTarget { get; set; }

    private int exitIndex;

    // Use this for initialization
    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);

        mana.Initialize(initMana, initMana);

        MyTarget = GameObject.Find("Enemy").transform;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

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
            exitIndex = 1;
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 0;
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 2;
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Block();

            if (!IsAttacking && !IsMoving && InLineOfSight())
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
        Vector3 targetDirection = new Vector3(MyTarget.position.x, 0.5f, MyTarget.position.z);

        float distance = Vector3.Distance(exitPoint.position, MyTarget.transform.position);

        Debug.DrawLine(exitPoint.position, targetDirection, Color.red);

        RaycastHit hit; 
            
        Physics.Raycast(exitPoint.position, targetDirection, out hit, distance, 256);

        if (hit.collider == null)
        {
            return true;
        }

        print(hit.collider.name);
        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        print("인덱스 : " + exitIndex + ", Block : " + blocks.Length);

        blocks[exitIndex].Activate();
    }
}

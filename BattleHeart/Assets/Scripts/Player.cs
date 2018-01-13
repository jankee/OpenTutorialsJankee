using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //[SerializeField]
    //private Stat health;

    [SerializeField]
    private Stat mana;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform exitPoint;

    private SpellBook spellBook;

    private Vector3 min, max;

    private float initMana = 50;

    public Transform MyTarget { get; set; }

    private int exitIndex;

    // Use this for initialization
    protected override void Start()
    {
        

        mana.Initialize(initMana, initMana);

        spellBook = this.GetComponent<SpellBook>();

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        //this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), transform.position.y, Mathf.Clamp(transform.position.z, min.z, max.z));

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
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);

        IsAttacking = true;

        myAnimator.SetBool("Attack", true);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript spell = Instantiate(newSpell.MySpellPrefab, exitPoint.position, Quaternion.identity).GetComponent<SpellScript>();

            spell.Initialize(currentTarget, newSpell.MyDamage);
        }

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();

        if (MyTarget != null && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }

    }

    //눈앞에 적을 찾는다
    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = new Vector3(MyTarget.position.x, 0.5f, MyTarget.position.z);

            float distance = Vector3.Distance(exitPoint.position, MyTarget.position);

            Debug.DrawLine(exitPoint.position, targetDirection, Color.red);

            RaycastHit hit;

            Physics.Raycast(exitPoint.position, targetDirection, out hit, distance, 256);

            if (hit.collider == null)
            {
                return true;
            }
        }
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

    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}

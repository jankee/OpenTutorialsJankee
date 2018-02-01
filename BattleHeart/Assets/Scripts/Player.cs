using System.Collections;
using UnityEngine;

public class Player : Character
{
    //[SerializeField]
    //private float initHealth;

    public Enemy MyTarget { get; set; }

    public Coroutine MyMoveRoutine { get; set; }

    private Coroutine attackRoutine;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform exitPoint;

    private SpellBook spellBook;

    // Use this for initialization
    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();

        MyMoveRoutine = null;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        return base.MoveRoutine(moveEnd, rotate);
    }

    public IEnumerator Attack(int spellIndex)
    {
        Enemy currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);

        MyIsAttacking = true;

        MyAnimator.SetBool("ATTACK", MyIsAttacking);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript spell = Instantiate(newSpell.MySpellPrefab, exitPoint.position, Quaternion.identity).GetComponent<SpellScript>();

            spell.Initialized(currentTarget, newSpell.MyDamage);
        }

        yield return new WaitForSeconds(newSpell.MyCastTime);

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();

        //공격, 움직임이 아니고 시야에 가리는게 없다면
        if (!MyIsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    //
    public bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            //에너미의 자식 중에 데미지 콜리전을 찾는다
            Vector3 targetDirection = MyTarget.transform.GetChild(0).GetChild(0).transform.position;

            float distance = Vector3.Distance(exitPoint.position, targetDirection);

            Debug.DrawLine(exitPoint.position, targetDirection, Color.yellow);

            Ray ray = new Ray(exitPoint.position, targetDirection);
            RaycastHit hitInfo;

            //레이케스트가 블럭 레이어 붙이치면
            Physics.Raycast(ray, out hitInfo, distance, 256);

            if (hitInfo.collider == null)
            {
                return true;
            }
        }
        return false;
    }

    public void Block()
    {
        foreach (Block block in blocks)
        {
            block.Deactivate();
        }
        print("Block");
        blocks[MyExitIndex].Activate();
    }

    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}
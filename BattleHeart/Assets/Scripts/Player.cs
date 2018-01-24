using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    public Stat MyHealth
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    [SerializeField]
    private float initHealth;

    public Enemy MyTarget { get; set; }

    public Coroutine MyMoveRoutine { get; set; }

    [SerializeField]
    private GameObject[] spellPrefabs;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform exitPoint;

    // Use this for initialization
    protected override void Start()
    {
        MyHealth.Initalize(initHealth, initHealth);

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //if (MyTarget != null)
        //{
        //    InLineOfSight();
        //}

        print(LayerMask.GetMask("Block"));

        base.Update();
    }

    public override IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        return base.MoveRoutine(moveEnd, rotate);
    }

    public IEnumerator Attack()
    {
        Block();

        MyAnimator.SetBool("ATTACK", true);

        MyIsAttacking = true;

        yield return new WaitForSeconds(1);

        print("Attack done");

        CastSpell();

        StopAttack();
    }

    public void CastSpell()
    {
        Instantiate(spellPrefabs[0], exitPoint.position, Quaternion.identity);
    }

    public bool InLineOfSight()
    {
        Vector3 targetDirection = MyTarget.transform.GetChild(0).GetChild(0).transform.position;

        float distance = Vector3.Distance(exitPoint.position, targetDirection);

        print("Target : " + targetDirection + " : " + MyTarget.transform.position + " : " + MyTarget.name);

        Debug.DrawLine(exitPoint.position, targetDirection, Color.yellow);

        Ray ray = new Ray(exitPoint.position, targetDirection);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, distance, 256);

        if (hitInfo.collider == null)
        {
            return true;
        }

        print(hitInfo.transform.name);

        return false;
    }

    private void Block()
    {
        foreach (Block block in blocks)
        {
            block.Deactivate();
        }

        blocks[MyExitIndex].Activate();
    }
}
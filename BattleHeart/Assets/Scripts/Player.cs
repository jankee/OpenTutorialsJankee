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

    public bool IsMoving
    {
        get
        {
            return false;
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        MyHealth.Initalize(initHealth, initHealth);

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

    public IEnumerator Attack()
    {
        if (!isAttacking && i)
        {
        }
        MyAnimator.SetBool("ATTACK", true);

        isAttacking = true;

        yield return new WaitForSeconds(3);

        StopAttack();
    }
}
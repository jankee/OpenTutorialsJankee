using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    private Player player;

    //protected Animator myAnimator;

    public Animator MyAnimator { get; set; }

    private Rigidbody myRigibody;

    private Vector3 direction;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected Stat health;

    public Stat MyHealth
    {
        get
        {
            return health;
        }
    }

    public bool IsAlive
    {
        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    public Transform MyTarget { get; set; }

    [SerializeField]
    private float initHealth = 100;

    public bool IsMoving
    {
        get
        {
            return Direction.x != 0 || Direction.z != 0;
        }
    }

    public bool IsAttacking { get; set; }

    // Use this for initialization
    protected virtual void Start()
    {
        health.Initialize(initHealth, initHealth);

        myRigibody = this.GetComponent<Rigidbody>();
        MyAnimator = this.GetComponent<Animator>();
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
        if (IsAlive)
        {
            myRigibody.velocity = Direction.normalized * Speed;
        }

    }

    public void HandleLayer()
    {
        if (IsAlive)
        {
            if (IsMoving)
            {
                ActivateLayer("WalkLayer");

                MyAnimator.SetFloat("X", Direction.x);
                MyAnimator.SetFloat("Z", Direction.z);
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
        else
        {
            ActivateLayer("DeathLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    //public virtual void StopAttack()
    //{
    //    IsAttacking = false;

    //    myAnimator.SetBool("Attack", IsAttacking);

    //    if (attackRoutine != null)
    //    {
    //        //자식의 코루틴을 접근하기 위해서
    //        StopCoroutine(attackRoutine);

    //        print("Attack Stoped");
    //    }
    //}

    public virtual void TakeDamage(float damage, Transform source)
    {
        //if (MyTarget == null)
        //{
        //    MyTarget = source;
        //}
         
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            Vector3 direction = Vector3.zero;

            myRigibody.velocity = direction;

            MyAnimator.SetTrigger("Die");

            GameManager.MyInstance.MyPlayer.MyTarget = null;
        }
    }
}

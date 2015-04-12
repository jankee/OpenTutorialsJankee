using UnityEngine;
using System.Collections;

public enum EnemyState
{
    none,
    move,
    attack,
    damage,
    dead,
}

public class Enemy : MonoBehaviour, IDamageable
{
    //적상태.
    public EnemyState currentState = EnemyState.none;
    //LineCast에 사용될 위치.
    public Transform FrontPosition;
    protected RaycastHit2D isObstacle;
    //이동 속도.
    public float moveSpeed = 1.0f;

    //체력.
    protected float currentHP = 10;
    protected float maxHP;

    //공격여부 저장.
    protected bool enableAttack = true;
    protected float attackPower = 10;

    protected float damagePower;
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        print("fire");
        Damage(10f);
    }



    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.none:
                //이동 중지
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case EnemyState.move:
                //이동
                

                isObstacle = Physics2D.Linecast(transform.position, 
                    FrontPosition.position, 1 << LayerMask.NameToLayer("Obstacle"));
                if (isObstacle)
                {
                    
                    //트루이면 공격을 한다.
                    if (isObstacle)
                    {
                        currentState = EnemyState.attack;
                        print("적의 상태 : " + currentState);
                        //애니메이터의 트리거 작동
                        animator.SetTrigger("attack");

                    }
                }
                else
                {
                    //거짓이면 이동
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                break;
            case EnemyState.attack:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case EnemyState.damage:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case EnemyState.dead:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
        }
    }

    void AttackAnimationEnd()
    {
        if (currentState == EnemyState.attack)
        {
            currentState = EnemyState.move;
        }
    }

    public void Damage(float damageTaken)
    {
        //dead나 none 상태일때 진행되지 않도록 한다.
        if (currentState == EnemyState.dead || currentState == EnemyState.none)
        {
            print("에너미 상태1 : " + currentState);
            if (IsInvoking("ChangeStateToMove"))
            {
                CancelInvoke("ChangeStateToMove");
            }
            return;    
        }

        //충돌 후 일정 시간 동안 이동 정지
        currentState = EnemyState.damage;
        print("에너미 상태2 : " + currentState);

        if (IsInvoking("ChangeStateToMove"))
        {
            print("IsIvoking Start");
            CancelInvoke("ChangeStateToMove");
        }

        Invoke("ChangeStateToMove", 1f);

        currentHP -= damageTaken;
        //현재 체력이 0보다 작다면
        if (currentHP <= 0)
        {
            currentHP = 0;
            enableAttack = false;
            currentState = EnemyState.dead;
            //데드 애니메이션 재생
            animator.SetTrigger("isDead");
            if (IsInvoking("ChangeStateToMove"))
            {
                CancelInvoke("ChangeStateToMove");
            }
            //TODO:점수 증가
        }
        else
        {
            animator.SetTrigger("damaged");
        }
    }

    void ChangeStateToMove()
    {
        print("ChangeStateToMove");
        currentState = EnemyState.move;
    }

    public void Attack()
    {
        //농장에 피해를 가한다
        RaycastHit2D findObstacle = Physics2D.Linecast(transform.position, FrontPosition.position,
            1 << LayerMask.NameToLayer("Obstacle"));
        if (findObstacle)
        {
            print("Fine Obstacle!!");

            IDamageable damageTarget = (IDamageable)findObstacle.transform.GetComponent(typeof(IDamageable));
            damageTarget.Damage(attackPower);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

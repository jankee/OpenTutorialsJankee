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

public class Enemy : MonoBehaviour 
{
    //적상태
    public EnemyState currentState = EnemyState.none;
    //LineCast에 사용될 위치
    public Transform FrontPosition;
    protected RaycastHit2D isObstacle;
    //이동 속도
    public float moveSpeed = 1.0f;

    //체력
    protected float currentHP;
    protected float maxHP;

    //공격여부 저장
    protected bool enableAttack = true;
    protected float attackPower = 10;

    protected float damagePower;
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.none:
                //이동 중지
                rigidbody2D.velocity = Vector2.zero;
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
                    rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
                }
                rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
                break;
            case EnemyState.attack:
                rigidbody2D.velocity = Vector2.zero;
                break;
            case EnemyState.damage:
                rigidbody2D.velocity = Vector2.zero;
                break;
            case EnemyState.dead:
                rigidbody2D.velocity = Vector2.zero;
                break;
        }
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

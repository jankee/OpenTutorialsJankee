using UnityEngine;
using System.Collections;

public enum EnemyState
{
    none,
    move,
    damage,
    dead,
}

public class Enemy : MonoBehaviour 
{
    //적상태
    EnemyState currentState = EnemyState.none;
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
        rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

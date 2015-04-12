﻿using UnityEngine;
using System.Collections;

public class FarmTouchControl : MonoBehaviour {

    //마우스 클릭으로 입력된 좌표를 공간좌표로 변환하는데 사용.
    public Camera MainCamera;
    //발사 할 게임 오브젝트
    public GameObject FireObj;
    //새총이 발사 되는 지점
    public Transform FirePoint;
    //발사 속도
    public float FireSpeed = 3f;
    //새총 발사할 방향
    Vector3 FireDirection;

    //발사 가능 여부 판다
    bool enableAttack = true;
    //마지막 사용자 입력위치 저장
    Vector3 lastInputPosition;

    //Vector3 계산에 사용
    Vector3 tempVector3;
    //Vector2 계산에 사용
    Vector2 tempVector2 = new Vector2();
    //새총 발사에 사용되는 오브젝트 처리
    GameObject tempObj;
    //애니메이터의 
    Animator animator;

    GameObject TEnemy;
    GameObject TsEnemy;

	// Use this for initialization
	void Start () 
    {
        
	}

    public void Awake()
    {
        animator = GetComponent<Animator>();

    }


	
	// Update is called once per frame
	void Update () 
    {
        //GetMouseButton 과 GetMouseButtonDown의 차이점이 많다.
        if (Input.GetMouseButton(0))
        {
            lastInputPosition = Input.mousePosition;

            if (enableAttack)
            {
                //공격 애니메이션 전환
                animator.SetTrigger("fire");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TEnemy = GameObject.Find("Pig");
            TEnemy.GetComponent<Enemy>().Damage(3);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TEnemy.GetComponent<Enemy>().Attack();
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TsEnemy = GameObject.Find("GamePlayManager");
            TsEnemy.GetComponent<GamePlayManager>().Damage(100);
            TsEnemy.GetComponent<GamePlayManager>().AddScore(10);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(FirePoint.position, tempVector3);
    }

    void FireTrigger()
    {
        //애니메이터에서 이벤트로 함수를 실행한다.
        Fire(lastInputPosition);

    }

    void Fire(Vector3 inputPosition)
    {
        tempVector3 = MainCamera.ScreenToWorldPoint(inputPosition);
        //기즈모 문에 z값을 캐릭터 위치로 바꿈
        tempVector3.z = this.GetComponent<Transform>().position.z;

        //벡터의 뺄셈 후 방향만 지닌 단위
        FireDirection = tempVector3 - FirePoint.position;
        FireDirection = FireDirection.normalized;
        //FireDirection.normalized를 모르겠다.
        print("파이어디렉션 :" + FireDirection);
        print("노말라이즈드 :" + FireDirection.normalized);

        tempObj = Instantiate(FireObj, FirePoint.position, Quaternion.LookRotation(FireDirection)) as GameObject;

        
        //발사한 오브젝트 속도 계산
        tempVector2.Set(FireDirection.x, FireDirection.y);
        tempVector2 = tempVector2 * FireSpeed;

        tempObj.GetComponent<Rigidbody2D>().velocity = tempVector2;

        enableAttack = false;
    }

    void FireEnd()
    {
        enableAttack = true;
    }
}

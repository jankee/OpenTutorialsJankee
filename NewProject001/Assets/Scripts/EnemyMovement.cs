using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement {

    void Start()
    {
        int xType = Random.Range(0, 2); // 0, 1
        int yType = Random.Range(0, 2);

        // 리턴변수 = (조건절) ? 참결과 : 거짓결과
        float xDir = (xType == 0) ? -1f : 1;
        float yDir = (yType == 0) ? -1f : 1;

        // 방향 설정
        direction = new Vector3(xDir, yDir, 0f);
    }

    // 이동 메소드
    public override void Move()
    {
        base.Move();

        Vector3 pos = transform.position;

        // 화면 경계 위치를 넘으면
        if (Mathf.Abs(pos.x) >= GameManager.LIMIT_POS_X)
        {
            // x방향 전환
            direction.x *= -1;
        }
        // 화면 경계 위치를 넘으면
        if (Mathf.Abs(pos.y) >= GameManager.LIMIT_POS_Y)
        {
            // x방향 전환
            direction.y *= -1;
        }

    }
}

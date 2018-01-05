using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovment : Movement {

    // Movement 클래스의 Move 메소드를 오버라이드(덮음) 함
    public override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector3(h, v, 0);

        // 이동을 수행
        // -> 이동 코드는 부모에 존재하기 때문에 부모 객체에 접근해야 함
        // base : 부모 객체에 접근하는 참조 키워드
        base.Move();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    // protected : 자식 외에는 접근을 불허함
    protected Vector3 direction;

    void Update()
    {
        Move();
    }

    // 자식이 Move 메소드를 오버라이드할 수 있음 (virtual)
    public virtual void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
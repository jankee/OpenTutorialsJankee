using UnityEngine;
using System.Collections;

public class EnemyShotObj : ShotObj 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //장애물과 충돌 시, 공격하여 피해를 가한다.
        AttackAndDestroy(other);
    }

}

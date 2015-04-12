using UnityEngine;
using System.Collections;

public class ShotObj : MonoBehaviour 
{
    protected float attackPower = 1;
    
    public void InitShotObj(float setupAttackPower)
    {
        attackPower = setupAttackPower;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //적 캐릭터인 경우, 공격하여 피해를 가한다.
        if (other.CompareTag("enemy") || other.CompareTag("boss"))
        {
            IDamageable damageTarget = (IDamageable)other.transform.GetComponent(typeof(IDamageable));
            damageTarget.Damage(attackPower);
            //공격 후 제거
            Destroy(gameObject);
        }
    }
}

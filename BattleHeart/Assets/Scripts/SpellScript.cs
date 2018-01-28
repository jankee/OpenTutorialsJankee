using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    //private Rigidbody MyRigidbody;

    [SerializeField]
    private float speed;

    public Enemy MyTarget { get; set; }

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(CastingSpell());
    }

    // Update is called once per frame
    private IEnumerator CastingSpell()
    {
        if (MyTarget != null)
        {
            //타겟의 HitBox 콜라이더를 찾는다
            Vector3 tmpTarget = MyTarget.Select().transform.position;
            //방향을 알기 위한 변수
            Vector3 direction = tmpTarget - this.transform.position;

            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(90f, 0f, angle);

            float distance = Vector3.Distance(transform.position, tmpTarget);

            while (distance > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tmpTarget, speed * Time.deltaTime);

                distance = Vector3.Distance(transform.position, tmpTarget);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" && other.transform == MyTarget.Select().transform)
        {
            print("HitBox");

            GetComponent<Animator>().SetTrigger("Effect");

            MyTarget = null;
        }
    }
}
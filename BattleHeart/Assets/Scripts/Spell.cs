using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    //private Rigidbody MyRigidbody;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; set; }

    // Use this for initialization
    private void Start()
    {
        //MyRigidbody = this.GetComponent<Rigidbody>();

        //target에 플레이어의 MyTarget에서 가져온다
        //MyTarget = InputManager.Instance.MyMovePlayer.MyTarget.transform;

        StartCoroutine(CastingSpell());
    }

    // Update is called once per frame
    private IEnumerator CastingSpell()
    {
        Vector3 direction = MyTarget.position - this.transform.position;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(90f, 0f, angle);

        float distance = Vector3.Distance(transform.position, MyTarget.position);

        while (distance > 0.5f)
        {
            //타겟의 자식 콜라이더를 찾아 줘야 한다
            transform.position = Vector3.MoveTowards(transform.position, MyTarget.position, speed * Time.deltaTime);

            distance = Vector3.Distance(transform.position, MyTarget.position);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GetComponent<Animator>().SetTrigger("Effect");
        }
    }
}
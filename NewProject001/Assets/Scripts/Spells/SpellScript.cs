using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private Rigidbody myRigibody;

    private Animator myAnimator;

    private Transform source;

    [SerializeField]
    private float speed;

    private float damage;

    public Transform MyTarget { get; private set; }

    // Use this for initialization
    void Start()
    {
        myRigibody = this.GetComponent<Rigidbody>();

        myAnimator = this.GetComponent<Animator>();
    }

    public void Initialize(Transform target, float damage, Transform source)
    {
        this.MyTarget = target;
        this.damage = damage;
        this.source = source;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector3 direction = MyTarget.position - transform.position;

            myRigibody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            this.transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" && other.transform == MyTarget)
        {
            Character cha = other.GetComponentInParent<Character>();

            speed = 0;

            cha.TakeDamage(damage, source);

            //other.GetComponentInParent<Enemy>().TakeDamage(damage);

            myAnimator.SetTrigger("Effect");

            myRigibody.velocity = Vector3.zero;

            MyTarget = null;
        }
    }

}

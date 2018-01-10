using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    private Rigidbody myRigibody;

    private Animator myAnimator;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; set; }

    // Use this for initialization
    void Start()
    {
        myRigibody = this.GetComponent<Rigidbody>();

        myAnimator = this.GetComponent<Animator>();
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
            myAnimator.SetTrigger("Effect");

            myRigibody.velocity = Vector3.zero;

            MyTarget = null;
        }
    }

}

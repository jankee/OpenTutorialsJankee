using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody myRigibody;

    [SerializeField]
    private float speed;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        myRigibody = this.GetComponent<Rigidbody>();

        target = GameObject.Find("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;

        myRigibody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        print(angle);

        this.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    
}

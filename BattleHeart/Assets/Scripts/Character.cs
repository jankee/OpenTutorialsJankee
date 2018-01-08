using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator animator;


    protected Vector3 direction;

    // Use this for initialization
    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    public void Move()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);

        AnimateMovement(direction);
    }

    public void AnimateMovement(Vector3 direction)
    {
        print(direction);

        animator.SetFloat("X", direction.x);

    }
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;

    private bool facingRight;


	// Use this for initialization
	void Start () 
    {
        facingRight = true;

        myRigidbody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();
	}

    void Update()
    {
        HandleInput();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        float horizontal = Input.GetAxis("Horizontal");

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        HandleMovement(horizontal);

        HandleAttack();

        Flip(horizontal);
	}

    private void HandleMovement(float horizontal)
    {

        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); 
            
        // Vector2.left;     x = -1
    }

    private void HandleAttack()
    {
        if (attack)
        {
            myAnimator.SetTrigger("attack");
            attack = false;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = this.transform.localScale;

            theScale.x *= -1;

            this.transform.localScale = theScale;
        }

        //if (horizontal < 0)
        //{
        //    facingRight = !facingRight;

        //    Vector3 theScale = this.transform.localScale;

        //    theScale.x = -1;

        //    this.transform.localScale = theScale;
        //}
        //else
        //{
        //    facingRight = true;

        //    Vector3 theScale = this.transform.localScale;

        //    theScale.x = 1;

        //    this.transform.localScale = theScale;
        //}
    }
}

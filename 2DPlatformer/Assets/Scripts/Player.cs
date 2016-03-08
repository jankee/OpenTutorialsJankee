using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;

    private bool slide;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

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

        isGrounded = IsGrounded();

        HandleMovement(horizontal);

        HandleAttack();

        ResetValue();

        Flip(horizontal);


        //test 방법
        //Collider2D test = Physics2D.OverlapCircle(groundPoints[0].position, groundRadius, whatIsGround);

        //if (test.gameObject != gameObject)
        //{
        //    jump = true;
        //}
        //jump = false;

        //print(jump);
	}

    private void HandleMovement(float horizontal)
    {
        if (!myAnimator.GetBool("slide") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && isGrounded)
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);     
        }

        

        if (isGrounded && jump)
        {
            print("jump : " + isGrounded);

            isGrounded = false;

            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        if (slide && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }
        
        // Vector2.left;     x = -1
    }

    private void HandleAttack()
    {
        if (attack && !myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");

            myRigidbody.velocity = Vector2.zero;
            //attack = false;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && facingRight == false || horizontal < 0 && facingRight == true)
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

    private void ResetValue()
    {
        attack = false;
        slide = false;
        jump = false;
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }    
        }
        return false;
    }
}

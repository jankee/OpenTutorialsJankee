using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    public Rigidbody2D MyRigibody { get; set; }

    public bool Attack { get; set; }

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

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

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

	// Use this for initialization
	void Start () 
    {
        facingRight = true;

        MyRigibody = GetComponent<Rigidbody2D>();

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

        OnGround = IsGrounded();

        HandleLayers();    

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

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
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

    //부울 값들을 리셋한다.

    private bool IsGrounded()
    {
        if (MyRigibody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }    
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround && Jump)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else if (OnGround && !Jump)
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}

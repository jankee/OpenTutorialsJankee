using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;


	// Use this for initialization
	void Start () 
    {
        facingRight = true;

        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

        float horizontal = Input.GetAxis("Horizontal");

        print(horizontal);

        HandleMovement(horizontal);

        Flip(horizontal);
	}

    private void HandleMovement(float horizontal)
    {

        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); 
            
        // Vector2.left;     x = -1
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && facingRight == false || horizontal < 0 && facingRight == true)
        {
            facingRight = !facingRight;

            Vector3 theScale = this.transform.localScale;

            theScale.x = -1;

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

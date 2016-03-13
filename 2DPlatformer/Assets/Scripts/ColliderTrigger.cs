using UnityEngine;
using System.Collections;

public class ColliderTrigger : MonoBehaviour 
{
    private BoxCollider2D playerCollider;

    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () 
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);

	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }

}

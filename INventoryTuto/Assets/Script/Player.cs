using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float speed;

    public Inventory inventory;


	// Use this for initialization
	void Start () 
    {
        //inventory = new Inventory();
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleMovement();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
    }

    public void OnCollisionEnter(Collider collision)
    {
        if (collision.tag == "Item")
        {
            inventory.AddItem(collision.GetComponent<Item>());

            Destroy(collision.gameObject);
        }
    }


    private void HandleMovement()
    {
        float translation = speed * Time.deltaTime;

        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }
}
 
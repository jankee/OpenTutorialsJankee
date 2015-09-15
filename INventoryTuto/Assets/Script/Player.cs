﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float speed;

    public Inventory inventory;

    private Inventory chest;


	// Use this for initialization
	void Start () 
    {
        //inventory = new Inventory();
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventory.Open();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (chest != null)
            {
                chest.Open();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
        }
        if (other.tag == "Chest")
        {
            chest = other.GetComponent<ChestInventory>().chestInventory;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Chest")
        {
            if (chest.IsOpen)
            {
                chest.Open();
            }

            chest = null;
            
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            inventory.AddItem(collision.gameObject.GetComponent<Item>());

            Destroy(collision.gameObject);
        }
    }


    private void HandleMovement()
    {
        float translation = speed * Time.deltaTime;

        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }
}
 
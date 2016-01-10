using UnityEngine;
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
            //Pick 0 or 1 or 2
            int randomType = UnityEngine.Random.Range(0, 3);

            GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

            int randomItem;

            switch (randomType)
            {
                case 0:
                    tmp.AddComponent<ItemScript>();
                    ItemScript newConsumeable = tmp.GetComponent<ItemScript>();
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
                    newConsumeable.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
                    inventory.AddItem(newConsumeable);
                    Destroy(tmp);
                    break;

                case 1:
                    tmp.AddComponent<ItemScript>();
                    ItemScript newWeapon = tmp.GetComponent<ItemScript>();
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                    newWeapon.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
                    inventory.AddItem(newWeapon);
                    Destroy(tmp);
                    break;

                case 2:
                    tmp.AddComponent<ItemScript>();
                    ItemScript newEquitment = tmp.GetComponent<ItemScript>();
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                    newEquitment.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
                    inventory.AddItem(newEquitment);
                    Destroy(tmp);
                    break;
            }
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
            inventory.AddItem(collision.gameObject.GetComponent<ItemScript>());

            Destroy(collision.gameObject);
        }
    }


    private void HandleMovement()
    {
        float translation = speed * Time.deltaTime;

        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }
}
 
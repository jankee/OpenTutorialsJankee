using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    public float speed;


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CombatTextManager.Instance.CreateText(transform.position);
        }
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontal, vertical) * (speed * Time.deltaTime));
	}
}

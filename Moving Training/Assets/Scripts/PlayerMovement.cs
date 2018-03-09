using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 10f;
	public float jumpPower = 500f;
	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void FixedUpdate()
	{
		var h = Input.GetAxisRaw("Horizontal");
		var v = Input.GetAxisRaw("Vertical");
		Move(h, v);

		if (Input.GetKeyDown(KeyCode.Space))
			Jump();
	}

	private void Move(float h, float v)
	{
		Vector3 movement = new Vector3(h, 0f, v);

		#region MovePosition

		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition(transform.position + movement);

		#endregion MovePosition

		#region AddForce

		/*
		movement = movement.normalized * speed;
		rb.AddForce(movement);
		*/

		#endregion AddForce
	}

	private void Jump()
	{
		rb.AddForce(Vector3.up * jumpPower);
	}
}
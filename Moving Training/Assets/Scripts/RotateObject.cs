using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotateObject : MonoBehaviour
{
	public Vector3 axis;
	private Rigidbody rb;

	private Vector3 currentAxis;

	public void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void FixedUpdate()
	{
		currentAxis += axis;
		rb.MoveRotation(Quaternion.Euler(currentAxis));
	}
}
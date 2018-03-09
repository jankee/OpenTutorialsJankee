using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveObject : MonoBehaviour
{
	public Vector3 startPos;
	public Vector3 goalPos;

	public float time;

	private Rigidbody rb;

	public void Awake()
	{
		rb = GetComponent<Rigidbody>();
		LaunchPingpong();
	}

	public void LaunchOnce()
	{
		StartCoroutine(MoveProcess(startPos, goalPos, time));
	}

	public void LaunchPingpong()
	{
		StartCoroutine(LoopProcess());
	}

	private IEnumerator LoopProcess()
	{
		for (;;)
		{
			yield return StartCoroutine(MoveProcess(startPos, goalPos, time));
			yield return StartCoroutine(MoveProcess(goalPos, startPos, time));
		}
	}

	private IEnumerator MoveProcess(Vector3 startPos, Vector3 goalPos, float time)
	{
		transform.position = startPos;

		var currentPos = Vector3.zero;
		for (float t = 0f; t < time; t += Time.deltaTime)
		{
			currentPos = Vector3.Lerp(startPos, goalPos, t / time);
			rb.MovePosition(currentPos);
			yield return null;
		}

		transform.position = goalPos;
	}
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothing = 5f;

	private Vector3 offset;

	public void Start()
	{
		offset = transform.position - target.position;
	}

	public void FixedUpdate()
	{
		if (target == null)
			return;

		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFloor : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Destroy(collision.gameObject);
		Invoke("Restart", 2f);
	}

	private void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
using UnityEngine;
using System.Collections;

public class EffectLoop : MonoBehaviour 
{

    
    public float timeAmount;

    float timeSpeed = 1f;
    float time = 0;


	// Update is called once per frame
	void Update () 
    {
        time = Mathf.Clamp(time + timeSpeed * Time.deltaTime, 0f, timeAmount);

        if (timeAmount == time)
        {
            StartCoroutine("ActiveEffect");
            time = 0f;
        }
	}

    IEnumerator ActiveEffect()
    {
        yield return new WaitForSeconds(1);

        NsEffectManager.RunReplayEffect(gameObject, true);
    }
}

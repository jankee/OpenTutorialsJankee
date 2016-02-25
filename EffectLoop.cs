using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class EffectLoopObj
{
	public GameObject loopObj;
	public float replayTime = 2f;
}

public class EffectLoop : MonoBehaviour 
{
    public float timeAmount;

    float timeSpeed = 1f;
    float time = 0;

	public EffectLoopObj[] effectLoopObjList;

	// Update is called once per frame
	void Update () 
    {
        time = Mathf.Clamp(time + timeSpeed * Time.deltaTime, 0f, timeAmount);

        if (timeAmount == time)
        {
            ActiveEffect();
            time = 0f;
        }
	}

	void OnEnable()
	{
		if(effectLoopObjList == null) return;
		if(effectLoopObjList.Length == 0) return;
		for (int i = 0; i < effectLoopObjList.Length; i++)
		{
			if(effectLoopObjList[i].loopObj != null)
				StartCoroutine ( LoopObjList( effectLoopObjList [i] ) );
		}
	}

	void OnDisable()
	{
		if(effectLoopObjList == null) return;
		if(effectLoopObjList.Length == 0) return;
		for (int i = 0; i < effectLoopObjList.Length; i++)
		{
			if(effectLoopObjList[i].loopObj != null)
				effectLoopObjList [i].loopObj.SetActive (false);
		}
	}

    void ActiveEffect()
    {
        NsEffectManager.RunReplayEffect(gameObject, true);
    }

	IEnumerator LoopObjList(EffectLoopObj obj)
	{
		while(true)
		{
			obj.loopObj.SetActive(true);
			yield return new WaitForSeconds(obj.replayTime);
			obj.loopObj.SetActive(false);
		}
	}
}

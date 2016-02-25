using UnityEngine;
using System.Collections;

public class CardChipEffect : MonoBehaviour 
{
	public delegate void OnFinished();
	public OnFinished onFinished;

	public GameObject effecChip;
	public GameObject effecChip1;
	public GameObject effecChip2;
	public GameObject effecChip3;

	GameObject effect = null;
	GameObject effect1 = null;
	GameObject effect2 = null;
	GameObject effect3 = null;

	public void CardChip()
	{
		if (effect == null) {
			effect = (GameObject)Instantiate (effecChip, new Vector3 (0, 0, 0), Quaternion.identity);
			effect.transform.parent = transform;
		} else {
			effect.SetActive (true);
			NsEffectManager.RunReplayEffect(effect,false);
		}
	}

	public void CardChip1()
	{
		if (effect1 == null) {
			effect1 = (GameObject)Instantiate (effecChip1, new Vector3 (0, 0, 0), Quaternion.identity);
			effect1.transform.parent = transform;
		} else {
			effect1.SetActive (true);
			NsEffectManager.RunReplayEffect(effect1,false);
		}
	}

	public void CardChip2()
	{
		if (effect2 == null) {
			effect2 = (GameObject)Instantiate (effecChip2, new Vector3 (0, 0, 0), Quaternion.identity);
			effect2.transform.parent = transform;
		} else {
			effect2.SetActive (true);
			NsEffectManager.RunReplayEffect(effect2,false);
		}
	}

	public void CardChip3()
	{
		if (effect3 == null) {
			effect3 = (GameObject)Instantiate (effecChip3, new Vector3 (0, 0, 0), Quaternion.identity);
			effect3.transform.parent = transform;
            effect3.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f); 
		} else {
			effect3.SetActive (true);
			NsEffectManager.RunReplayEffect(effect3,false);
		}
	}

	public void CardChipAnimationEndEvent()
	{
		if (effect != null)
			effect.SetActive (false);
		
		if (effect1 != null)
			effect1.SetActive (false);

		if (effect2 != null)
			effect2.SetActive (false);

		if (effect3 != null)
			effect3.SetActive (false);

		if (onFinished != null)
			onFinished ();
	}
}

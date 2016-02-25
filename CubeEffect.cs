using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HRBall.DataModel;

public class CubeEffect : MonoBehaviour 
{
	public delegate void OnShow2D();
	public OnShow2D onShow2D;
	public delegate void OnFinished();
	public OnFinished onFinished;

	public GameObject cubeEff;
	public GameObject cubeEff01;
	public GameObject cubeEff02;
	public GameObject cubeEff03;

	List<GameObject> cubeObj;
	List<Vector3> EffRotate;
	GameObject cubeStar0 = null;
	GameObject cubeStar1 = null;
	GameObject cubeStar2 = null;

	// Use this for initialization
	void Start () 
	{
		cubeObj = new List<GameObject>();
		EffRotate = new List<Vector3> ();
		EffRotate.Add (new Vector3(-90f, 90f, 0f));
		EffRotate.Add (new Vector3(-90f, -180f, 0f));
		EffRotate.Add (new Vector3(90f, 90f, 0f));
		EffRotate.Add (new Vector3(90f, 0f, 0f));
		EffRotate.Add (new Vector3(0f, 0f, 90f));
		EffRotate.Add (new Vector3(0f, 0f, -180f));
		EffRotate.Add (new Vector3(0f, 0f, -90f));
		EffRotate.Add (new Vector3(0f, 90f, -90f));

		for (int i = 0; i < 8; i++) 
		{
			cubeObj.Add(GameObject.Find("fx_cudeLight_0" + (i + 1))); 
		}

		for (int i = 0; i < 8; i++) 
		{
			GameObject cubeE = Instantiate (cubeEff, Vector3.zero, Quaternion.identity) as GameObject;
			cubeE.transform.parent = cubeObj[i].transform;
			cubeE.transform.localRotation = Quaternion.Euler(EffRotate[i]);
			cubeE.transform.name = "fx_0" + (i + 1).ToString();
			cubeE.transform.position = cubeObj[i].transform.localPosition;
			//cubeE.transform.rotation = Quaternion.Euler(EffRotate[i]);
			cubeE.transform.localScale = new Vector3(1, 1, 1);
		}
	}
	
	// Update is called once per frame
	public void StartEff () 
	{
		if (cubeStar0 == null) {
			cubeStar0 = Instantiate (cubeEff01, Vector3.zero, Quaternion.identity) as GameObject;
			cubeStar0.transform.parent = GameObject.Find ("fx_cubeEff_01").transform;
		} else {
			cubeStar0.SetActive (true);
			NsEffectManager.RunReplayEffect (cubeStar0, false);
		}
	}

	public void StartEff01 () 
	{
		if (cubeStar1 == null) {
			cubeStar1 = Instantiate (cubeEff02, Vector3.zero, Quaternion.identity) as GameObject;
			cubeStar1.transform.parent = GameObject.Find ("fx_cubeEff_01").transform;
		} else {
			cubeStar1.SetActive (true);
			NsEffectManager.RunReplayEffect (cubeStar1, false);
		}
	}

	public void StartEff02 () 
	{
		if (cubeStar2 == null) {
			cubeStar2 = Instantiate(cubeEff03);
			cubeStar2.transform.localPosition = Vector3.zero;
//			cubeStar2 = Instantiate (cubeEff03, Vector3.zero, Quaternion.identity) as GameObject;
			cubeStar2.transform.parent = GameObject.Find ("fx_cubeEff_01").transform;
		} else {
			cubeStar2.SetActive (true);
			NsEffectManager.RunReplayEffect(cubeStar2,false);
		}
//		cubeStar2.transform.forward = 
//		cubeStar2.transform.LookAt = transform.Find ("CamRootDerummy/Camera001/Camera").LookAt;
		cubeStar2.transform.rotation = Quaternion.LookRotation (transform.Find ("CamRootDerummy/Camera001/Camera").position - cubeStar2.transform.position);
	}

	public void Show2DUI()
	{
		if (onShow2D != null)
			onShow2D ();
	}

	public void CubeAnimationEndEvent()
	{
		if (cubeStar0 != null)
			cubeStar0.SetActive (false);

		if (cubeStar1 != null)
			cubeStar1.SetActive (false);

		if (cubeStar2 != null)
			cubeStar2.SetActive (false);

		if (onFinished != null)
			onFinished ();
	}
}

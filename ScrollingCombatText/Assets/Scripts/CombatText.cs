﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

    private float speed;

    private Vector3 direction;

    private float fadeTime;

    public AnimationClip critAnim;

    private bool criti;

	// Update is called once per frame
	void Update () 
    {
        float translation = speed * Time.deltaTime;

        this.transform.Translate(direction * translation);
	}

    public void Initialize(float speed, Vector3 direction, float fadeTime, bool criti)
    {
        this.speed = speed;
        this.direction = direction;
        this.fadeTime = fadeTime;
        this.criti = criti;

        if (criti)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            StartCoroutine("Critical");
        }
        else
        {
            StartCoroutine("FadeOut");
        }

        
    }

    private IEnumerator Critical()
    {
        yield return new WaitForSeconds(critAnim.length);

        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = GetComponent<Text>().color.a;

        float rate = 1.0f / fadeTime;

        float progress = 0f;

        while (progress < 1)
        {
            Color tmpColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress));

            progress += rate * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
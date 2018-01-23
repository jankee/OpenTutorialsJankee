﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text currentSpell;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    


    public Spell CastSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        castingBar.color = spell.MyBarColor;

        currentSpell.text = spell.MyName;

        icon.sprite = spell.MyIcon;

        spellRoutine = StartCoroutine(Progress(spell));

        //코루틴 저장
        fadeRoutine = StartCoroutine(FadeBar());
        castTime.text = spell.MyCastTime.ToString();

        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;

        float rate = 1f / spell.MyCastTime;

        float progress = 0f;

        while (progress <= 1)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (spell.MyCastTime - timePassed).ToString("F2");

            if ((spell.MyCastTime - timePassed) < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }
        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1f / 0.5f;

        float progress = 0f;

        while (progress <= 1)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            //castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);

            canvasGroup.alpha = 0;

            fadeRoutine = null;
        }

        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);

            spellRoutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        return spell;
    }
}

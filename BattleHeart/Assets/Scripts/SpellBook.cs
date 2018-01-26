using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text castTimeTxt;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public Spell CastSpell(int index)
    {
        castingBar.color = spells[index].MyBarColor;

        castingBar.fillAmount = 1f;

        castTimeTxt.text = spells[index].MyCastTime.ToString("F2") + "/" + spells[index].MyCastTime.ToString("F2");

        icon.sprite = spells[index].MyIcon;

        spellRoutine = StartCoroutine(Progress(index));

        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / spells[index].MyCastTime;

        float progress = 0f;

        while (progress <= 1f)
        {
            castingBar.fillAmount = Mathf.Lerp(1f, 0f, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTimeTxt.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if (spells[index].MyCastTime - timePassed < 0)
            {
                castTimeTxt.text = "0.00";
            }

            yield return null;
        }

        StopCasting();
    }

    //아직 사용하지 않음
    private IEnumerator FadeBar()
    {
        float timeLeft = Time.deltaTime;

        float rate = 1.0f / 0.5f;

        float progress = 0f;

        while (progress <= 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Coroutine spellRoutine;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Spell CastSpell(int index)
    {
        castingBar.color = spells[index].MyBarColor;

        spellName.text = spells[index].MyName;

        icon.sprite = spells[index].MyIcon;

        spellRoutine = StartCoroutine(Progress(index));

        StartCoroutine(FadeBar());

        castTime.text = spells[index].MyCastTime.ToString();

        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;

        float rate = 1f / spells[index].MyCastTime;

        float progress = 0f;

        while (progress <= 1)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if ((spells[index].MyCastTime - timePassed) < 0)
            {
                castTime.text = "0.00";
            }

            //timeLeft += Time.deltaTime;

            //if (timeLeft <= spells[index].MyCastTime)
            //{
            //    castTime.text = timeLeft.ToString();
            //}
            //else
            //{
            //    castTime.text = spells[index].MyCastTime.ToString();
            //}

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
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);

            spellRoutine = null;
        }
    }
}

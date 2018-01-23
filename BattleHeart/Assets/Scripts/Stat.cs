using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Text statValue;

    private float currentFill;

    private float currentValue;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0f;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;

            statValue.text = currentValue + " / " + MyMaxValue;
        }
    }

    public float MyMaxValue { get; set; }

    // Use this for initialization
    private void Start()
    {
        content = this.GetComponent<Image>();
    }

    private void Update()
    {
        //currentFill 값과 content.fillAmount 값이 다르면
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    // Update is called once per frame
    public void Initalize(float currentValue, float maxValue)
    {
        this.MyMaxValue = maxValue;
        this.MyCurrentValue = currentValue;
    }
}
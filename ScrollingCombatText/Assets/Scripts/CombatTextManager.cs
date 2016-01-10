using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour 
{
    private static CombatTextManager instance;
    public static CombatTextManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance; 
        }
    }

    public GameObject TextObj;

    public RectTransform canvasTransform;

    public float speed;

    public Vector3 direction;

    public float fadeTime;

    public bool criti;

    public void CreateText(Vector3 position, string text, Color color, bool criti)
    {
        GameObject tmp = (GameObject)Instantiate(TextObj, position, Quaternion.identity);

        tmp.transform.SetParent(canvasTransform);

        tmp.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);

        tmp.GetComponent<CombatText>().Initialize(speed, direction, fadeTime, criti);

        tmp.GetComponent<Text>().text = text;

        tmp.GetComponent<Text>().color = color;
    }

}

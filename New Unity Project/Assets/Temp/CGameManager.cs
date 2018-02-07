using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGameManager : MonoBehaviour
{
    public Text scoreText;

    // Use this for initialization
    private void Start()
    {
        scoreText.text = "SCORE : " + PlayerPrefs.GetString("SCORE", "0");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
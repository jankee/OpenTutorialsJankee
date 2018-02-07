using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUFO : MonoBehaviour
{
    public Text playerNameText;

    public Sprite[] sprites;

    // Use this for initialization
    private void Start()
    {
        playerNameText.text = PlayerPrefs.GetString("USER_ID", "PLAYER");

        string type = PlayerPrefs.GetString("TYPE", "0");

        GetComponent<SpriteRenderer>().sprite = sprites[int.Parse(type)];
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
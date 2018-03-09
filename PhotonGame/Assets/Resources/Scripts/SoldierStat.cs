using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierStat : Photon.MonoBehaviour
{
    public enum STATE { IDLE, MOVE, ATTACK, DEATH }

    public STATE state = STATE.IDLE;

    public Text playerNameText;

    public Text scoreText;

    // Use this for initialization
    private void Start()
    {
        playerNameText.text = photonView.owner.NickName;
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = photonView.owner.GetScore().ToString();
    }
}
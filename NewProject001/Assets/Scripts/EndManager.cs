using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관라자 사용

public class EndManager : MonoBehaviour {

    public Text scoreText; // 점수 텍스트

	// Use this for initialization
	void Start () {

        // string 문자열변수 = int변수.ToString();
        // -> ToString() : int값을 string으로 변경해줌
        scoreText.text = GameManager.score.ToString();
	}
	
    public void OnReStartButtonClick()
    {
        // 게임씬을 로드함(전환함)
        SceneManager.LoadScene("Game");
    }

}

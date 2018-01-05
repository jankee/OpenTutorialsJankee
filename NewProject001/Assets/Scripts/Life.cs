using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 사용
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public int lifeCnt;

    // C# 배열 생성
    // 타입[] 배열변수 = new 타입[배열갯수];
    // Image[] lifeIcons = new Image[5];
    public Image[] lifeIcons;

    private void Start()
    {
        // 생명 아이콘의 갯수를 생명 카운터로 지정함
        lifeCnt = lifeIcons.Length;
    }

    // 생명 감소
    public void LifeCntDown()
    {
        lifeCnt--; // 생명 카운트 감소

        // 생명 카운트와 일치하는 생명 이미지를 투명하게 처리함
        lifeIcons[lifeCnt].color = Color.clear;


        if (lifeCnt <= 0)
        {
            GameManager.GameOver();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 충돌 처리 클래스
public class Collision : MonoBehaviour {

    public Life life; // Life 컴포넌트 참조

    public GameManager gameManager;

    // 충돌이 시작됨
    // Collider2D 매개변수 : 충돌체의 Collider 컴포넌트 참조
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌 시 보석, 적군을 파괴함
        Destroy(collision.gameObject);

        //if (collision.tag == "Enemy")

        if (collision.tag.Equals("Enemy"))
        {
            Debug.Log("적군이랑 충돌함!!");

            // 적군 생성 카운트 감소
            GameManager.enemyCnt--;

            life.LifeCntDown(); // 생명 아이콘 감소
        }
        else if (collision.tag.Equals("Jewel"))
        {
            Debug.Log("보석이랑 충돌함!!");

            // 보석 갯수 감소
            GameManager.jewelCnt--;

            // 점수 증가
            gameManager.ScoreUp();
        }
    }

    // 충돌이 되고 있음
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D!!");
    }
    */

    // 충돌이 끝났음
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D!!");
    }

}

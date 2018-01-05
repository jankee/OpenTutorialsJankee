using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // UI사용
using UnityEngine.SceneManagement; // 씬관리자 사용

public class GameManager : MonoBehaviour {

    public static float LIMIT_POS_X = 5.9f;
    public static float LIMIT_POS_Y = 4.2f;

    public float createJewelDealyTime; // 보석 생성 지연 시간
    public static int jewelCnt = 0; // 보석 생성 갯수
    public GameObject jewelPrefab; // 보석 프리팹 참조 변수
    public int maxJewelCnt; // 최대 보석 생성 갯수

    public float createEnemyDealyTime; // 적군 생성 지연 시간
    public static int enemyCnt = 0; // 적군 생성 갯수
    public GameObject enemyPrefab; // 적군 프리팹 참조 변수
    public int maxEnemyCnt; // 최대 적군 생성 갯수

    public Text scoreText; // 점수 텍스트

    public static int score = 0; // 게임 점수

    // Use this for initialization
    void Start () {

        // 게임 점수를 초기화함
        score = 0;

        // InvokeRepeating() : 특정 메소드를 반복 호출 해주는 메소드

        // 보석 생성용 타이머 생성
        InvokeRepeating("JewelCreate", 2f, createJewelDealyTime);
        // 적군 생성용 타이머 생성
        InvokeRepeating("EnemyCreate", 2f, createEnemyDealyTime);
    }
	
    // 오브젝트 생성 메소드
    public void GameObjectCreate(GameObject prefab)
    {
        float x = Random.Range(-LIMIT_POS_X, LIMIT_POS_X);
        float y = Random.Range(-LIMIT_POS_Y, LIMIT_POS_Y);

        Vector3 pos = new Vector3(x, y, 0f);

        // 매개변수로 받은 프리팹을 게임 랜덤한 위치에 생성함
        Instantiate(prefab, pos, Quaternion.identity);
    }

    // 오브젝트 생성 메소드
    public void EnemyCreate()
    {
        // 현재 적군의 숫자가 최대 숫자보다 작으면
        if (maxEnemyCnt > enemyCnt)
        {
            // 적군을 생성함
            GameObjectCreate(enemyPrefab);
            enemyCnt++; // 적군 생성 카운트 증가
        }
    }

    // 오브젝트 생성 메소드

    public void JewelCreate()
    {
        // 현재 보석 카운트가 최대 숫자보다 크면 함수 실행을 중단함
        if (maxJewelCnt < jewelCnt) return;

        GameObjectCreate(jewelPrefab);

        jewelCnt++; // 보석 생성 카운트 증가
    }

    public void ScoreUp()
    {
        // int 숫자변수 = int.Parse("숫자문자열");
        score = int.Parse(scoreText.text);

        score += 10; // 점수 증가

        scoreText.text = score.ToString();
    }

    public static void GameOver()
    {
        // 종료씬으로 이동
        SceneManager.LoadScene("End");
    }
}

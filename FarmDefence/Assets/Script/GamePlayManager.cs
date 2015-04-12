using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
    ready,
    idle,
    gameOver,
    Wait,
}

public class GamePlayManager : MonoBehaviour, IDamageable
{
    //게임 상황 판단
    public GameState nowGameState = GameState.ready;

    //생성할 Enemy 게임 오브젝트 리스트
    public List<GameObject> spawnEnemyObjs = new List<GameObject>();

    //적 생성할 위치 저장.
    List<Vector3> spawnPositions = new List<Vector3>
    {
        new Vector3(12f, 2.7f, 0f), new Vector3(12f, 0.26f, 0f),
        new Vector3(12f, -2.2f, 0f), new Vector3(12f, -4.7f, 0f),
    };

    float farmCurrentHP = 300;
    float farmLimitHP = 300;

    //게임 시작 후 경과 시간
    float timeElapsed = 0;

    //획득한 점수 저장
    int score = 0;

    //게임 오브젝트 풀에 들어가는 게임 오브젝트의 최초 생성되는 위치
    public Transform gameObjectPoolPosition;
    //게임 오브젝트 풀 딕셔너리.
    Dictionary<string, GameObjectPool> gameObjectPool = new Dictionary<string, GameObjectPool>();

    public void OnEnable()
    {
        InitGameObjectPools();
    }

    void InitGameObjectPools()
    {
        print("Hi");
        for (int i = 0; i < spawnEnemyObjs.Count; i++)
        {
            // 게임 오브젝트 풀 생성
            GameObjectPool tempGameObjectPool = new GameObjectPool
            (gameObjectPoolPosition.transform.position.x, spawnEnemyObjs[i]);
            for (int j = 0; j < 5; j++)
            {
                //게임 오브젝트 생성
                GameObject tempEnemyObj = Instantiate
                    (spawnEnemyObjs[i], gameObjectPoolPosition.position, Quaternion.identity) as GameObject;
                tempEnemyObj.name = spawnEnemyObjs[i].name + j;
                tempEnemyObj.transform.parent = gameObjectPoolPosition;
                //게임 오브젝트를 풀에 등록
                tempGameObjectPool.AddGameObjec(tempEnemyObj);

            }
        }
    }

    public void Awake()
    {
        //스크립트 연결.
        GameData.Instance.gamePlayManager = this;
    }

    public void OnDestroy()
    {
        GameData.Instance.gamePlayManager = null;
    }


    public void Damage(float damageTaken)
    {

        if (nowGameState == GameState.gameOver)
        {
            print(nowGameState);
            return;
        }

        farmCurrentHP -= damageTaken;
        print(farmCurrentHP);

        if (farmCurrentHP <= 0)
        {
            nowGameState = GameState.gameOver;
        }
    }

    public void AddScore(int addScore)
    {
        if (nowGameState == GameState.ready || nowGameState == GameState.gameOver)
        {
            return;
        }

        score += addScore;

        print("게임 플레이 메니저 스코어 : " + score);
    }
}

public class GameObjectPool
{
    int poolNowIndex = 0;
    int count = 0;
    float spawnPositionX = 0;
    public GameObject SpawnObj;

    List<GameObject> pool = new List<GameObject>();

    // 생성자
    public GameObjectPool(float PositionX, GameObject initSpawnObj)
    {
        spawnPositionX = PositionX;
        SpawnObj = initSpawnObj;
    }

    //게임 오브젝트를 풀에 추가 한다.
    public void AddGameObjec(GameObject addGameObject)
    {
        pool.Add(addGameObject);
        count++;
    }

    //사용하지 않은 게임오브젝트를 선택한다.
    public bool NextGameObject(out GameObject returnObject)
    {
        int startIndexNo = poolNowIndex;
        if (lastIndex == 0)
        {
            returnObject = default(GameObject);
            return false;
        }

        while (pool[poolNowIndex].transform.position.x < spawnPositionX)
        {
            poolNowIndex++;
            poolNowIndex = (poolNowIndex >= count) ? 0 : poolNowIndex;

            //사용가능한 게임 오브젝트가 없을 때
            if (startIndexNo == poolNowIndex)
            {
                returnObject = default(GameObject);
                return false;
            }
        }
        returnObject = pool[poolNowIndex];
        return true;
    }

    public int lastIndex
    {
        get
        {
            return pool.Count;
        }
    }

    //해당 인덱스의 게임 오브젝트가 존재하는 경우 반환
    public bool GetObject(int index, out GameObject obj)
    {
        if (lastIndex < index || pool[index] == null)
        {
            obj = default(GameObject);
            return false;
        }

        obj = pool[index];
        return true;
    }
}

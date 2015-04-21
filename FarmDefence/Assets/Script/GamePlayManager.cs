using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

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
        new Vector3(12f, 2.7f, 190f), new Vector3(12f, 0.26f, 190f),
        new Vector3(12f, -2.2f, 190f), new Vector3(12f, -4.7f, 190f),
    };

    //농장 HP
    float farmCurrentHP = 300;
    float farmLimitHP = 300;

    //게임 시작 후 경과 시간
    float timeElapsed = 0;

    //획득한 점수 저장
    int score = 0;

    //게임 오브젝트 풀에 들어가는 게임 오브젝트의 최초 생성되는 위치
    public Transform gameObjectPoolPosition;

    //게임 오브젝트 풀 딕셔너리.
    Dictionary<string, GameObjectPool> gameObjectPools = new Dictionary<string, GameObjectPool>();

    //적 생성 데이터 저장
    List<EnemyWaveData> enemyWaveDatas = new List<EnemyWaveData>();
    int currentEnemyWaveDataIndexNo = 0;

    //생성할 위치값을 생성할 유닛 수로 치환
    Dictionary<int, int> positionToAmount = new Dictionary<int, int>
    {
        { 1, 1}, { 2, 1}, { 4, 1}, { 8, 1},
        { 3, 2}, { 5, 2}, { 6, 2}, { 9, 2}, {10, 2}, {12, 2},
        { 7, 3}, {11, 3}, {13, 3}, {14, 3},
        {15, 4}
    };

    void Awake()
    {
        //스크립트 연결
        GameData.Instance.gamePlayManager = this;
    }

    void OnDestroy()
    {
        //스크립트 연결 해제
        GameData.Instance.gamePlayManager = null;
    }

    public void OnEnable()
    {
        InitGameObjectPools();
        LoadEnemyWaveDataFromXML();
    }

    public void Update()
    {
        switch (nowGameState)
        {
            case GameState.ready:
                //게임이 시작되면 3초간 사용자에게 준비시간을 제공
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= 3.0f)
                {
                    timeElapsed = 0;
                    SetupGameStateToIdle();
                }
                break;
            case GameState.idle:
                break;
            case GameState.gameOver:
                break;
            case GameState.Wait:
                break;
            default:
                break;
        }
    }

    public void SetupGameStateToIdle()
    {
        //게임 스테이트를 idle로 변경
        nowGameState = GameState.idle;

        //해체되지 못한 Invoke를 해제하고 새롭게 설정
        if (IsInvoking("CheckSpawnEnemy"))
        {
            CancelInvoke("CheckSpawnEnemy");
        }
        InvokeRepeating("CheckSpawnEnemy", 0.5f, 2.0f);
    }

    void CheckSpawnEnemy()
    {
        //idle 상태가 아니라면 더 이상 진행되지 못하도록 에러처리
        if (nowGameState != GameState.idle)
        {
            return;
        }

        //적 생성 데이터 전체가 소모되었다면 게임을 종료하도록 한다
        if (currentEnemyWaveDataIndexNo >= enemyWaveDatas.Count)
        {
            nowGameState = GameState.gameOver;
            CancelInvoke("CheckSpawnEnemy");
            return;
        }

        //적을 생성한다.
        SpawnEnemy(enemyWaveDatas[currentEnemyWaveDataIndexNo]);
        //생성된 적이 boss인 경우 적 생성을 멈춘다
        if (enemyWaveDatas[currentEnemyWaveDataIndexNo].tagName == "boss")
        {
            nowGameState = GameState.Wait;
            CancelInvoke("CheckSpawnEney");
        }

        currentEnemyWaveDataIndexNo++;
    }


    void InitGameObjectPools()
    {
        for (int i = 0; i < spawnEnemyObjs.Count; i++)
        {
            // 게임 오브젝트 풀 생성
            GameObjectPool tempGameObjectPool = new GameObjectPool
            (gameObjectPoolPosition.position.x, spawnEnemyObjs[i]);
            for (int j = 0; j < 5; j++)
            {
                //게임 오브젝트 생성
                GameObject tempEnemyObj = Instantiate
                    (spawnEnemyObjs[i], spawnPositions[Random.Range(0, 4)], Quaternion.identity) as GameObject;
                tempEnemyObj.name = spawnEnemyObjs[i].name + j;
                tempEnemyObj.transform.parent = gameObjectPoolPosition;
                //게임 오브젝트를 풀에 등록
                tempGameObjectPool.AddGameObjec(tempEnemyObj);

            }
        }
    }

    void LoadEnemyWaveDataFromXML()
    {
        print("LoadEnemyWaveDataFromXML");
        //이미 데이터가 로딩되었다면 다시 로딩하지 못하도록 예외처리
        if (enemyWaveDatas != null && enemyWaveDatas.Count > 0)
        {
            return;
        }

        //XML파일을 읽는다
        TextAsset xmlText = Resources.Load("EnemyWaveData") as TextAsset;
        //XML파일을 문서 객체 모델(DOM)로 전환한다
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(xmlText.text);
        //XML파일 안에 EnemyWaveData란 XmlNode를 모두 읽어 드린다.
        XmlNodeList nodeList = xDoc.DocumentElement.SelectNodes("EnemyWaveData");


        XmlSerializer serializer = new XmlSerializer(typeof(EnemyWaveData));

        //역질렬화를 통해 EnemyWaveData 구조체로 변경하여 enemyWaveDatas 멤버 필드에 저장을 한다.
        for (int i = 0; i < nodeList.Count; i++)
        {
            EnemyWaveData enemyWaveData = (EnemyWaveData)serializer.Deserialize(new XmlNodeReader(nodeList[i]));
            enemyWaveDatas.Add(enemyWaveData);
        }
    }

    void SpawnEnemy(EnemyWaveData enemyData)
    {
        int positionPointer = 1;
        int shiftPosition = 0;

        //생성할 위치 값으로 생성할 유닛 수 판다
        enemyData.amount = positionToAmount[enemyData.spawnPosition];

        //생성해야하는 숫자만큼 loop
        for (int i = 0; i < enemyData.amount; i++)
        {
            //생성할 위치 선택
            while ((positionPointer & enemyData.spawnPosition) > 1)
            {
                shiftPosition++;
                positionPointer = 1 << shiftPosition;
            }

            //오브젝트 풀에 사용 가능한 게임 오브젝트가 있는지 점검
            GameObject currentSpawnGameObject;

            


            if (!gameObjectPools[enemyData.type].NextGameObject(out currentSpawnGameObject))
            {
                //사용가능한 게임 오브젝트가 없다면 생성하여 추가한다.
                currentSpawnGameObject = Instantiate
                    (gameObjectPools[enemyData.type].SpawnObj, gameObjectPoolPosition.transform.position,
                    Quaternion.identity) as GameObject;

                currentSpawnGameObject.transform.parent = gameObjectPoolPosition;
                currentSpawnGameObject.name =
                    enemyData.type + gameObjectPools[enemyData.type].lastIndex;
                gameObjectPools[enemyData.type].AddGameObjec(currentSpawnGameObject);
            }
            currentSpawnGameObject.transform.position = spawnPositions[shiftPosition];

            //선택된 적 캐릭터를 초기화하여 작동시킨다
            currentSpawnGameObject.tag = enemyData.tagName;
            Enemy currentEnemy = currentSpawnGameObject.GetComponent<Enemy>();
            currentEnemy.InitEnemy(enemyData.HP, enemyData.AD, enemyData.MS);
            
            shiftPosition++;

            if (enemyData.tagName == "boss")
            {
                
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
    float spawnPosition;
    public GameObject SpawnObj;

    List<GameObject> pool = new List<GameObject>();

    // 생성자
    public GameObjectPool(float PositionX, GameObject initSpawnObj)
    {
        spawnPosition = PositionX;
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

        while (pool[poolNowIndex].transform.position.x < spawnPosition)
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

[XmlRoot]
public struct EnemyWaveData
{
    [XmlAttribute("waveNo")]
    public int waveNo;
    [XmlElement]
    public string type;
    [XmlElement]
    public int amount;
    [XmlElement]
    public int spawnPosition;

    [XmlElement]
    public string tagName;

    [XmlElement]
    public float MS;
    [XmlElement]
    public float AD;
    [XmlElement]
    public float HP;
}

using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy 
{
    //발사할 게임 오브젝트
    public GameObject shotObj;
    //발사 위치
    public Transform firePosition;
    //발사 속도
    public float fireSpeed = 3;

    //shot gameObject pool
    GameObjectPool objPool;
    Vector3 spawnPos = new Vector3(0, 50, 0);

    //발사할 게임 오브젝트 생성에 사용
    GameObject tempObj;
    Vector2 tempVector2 = new Vector2();

    public override void Attack()
    {
        //원거리에서 공격할 수 있도록 구현
        if (objPool == null)
        {
            objPool = new GameObjectPool(GameData.Instance.gamePlayManager.gameObjectPoolPosition.position.x, shotObj);
        }
        if (!objPool.NextGameObject(out tempObj))
        {
            tempObj = Instantiate(shotObj, GameData.Instance.gamePlayManager.gameObjectPoolPosition.transform.position,
                Quaternion.identity) as GameObject;
            tempObj.name = shotObj.name + objPool.lastIndex;
            objPool.AddGameObjec(tempObj);
            
        }
        //자리 이동
        tempObj.transform.position = firePosition.position;
        //속도 지정
        tempVector2 = Vector2.right * -1 * fireSpeed;
        tempObj.GetComponent<Rigidbody2D>().velocity = tempVector2;

        EnemyShotObj tempEnemyShot = tempObj.GetComponent<EnemyShotObj>();
        tempEnemyShot.InitShotObj(attackPower);
    }

	// Use this for initialization
	public void CallAttack () 
    {
        Attack();	
	}
}

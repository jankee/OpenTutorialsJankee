using UnityEngine;
using System.Collections;

public class FarmTouchControl : MonoBehaviour {

    //마우스 클릭으로 입력된 좌표를 공간좌표로 변환하는데 사용.
    public Camera MainCamera;
    //발사 할 게임 오브젝트
    public GameObject FireObj;
    //새총이 발사 되는 지점
    public Transform FirePoint;
    //발사 속도
    public float FireSpeed = 3;

    //발사 가능 여부 판다
    bool enableAttack = true;
    //마지막 사용자 입력위치 저장
    Vector3 lastInputPosition;

    //Vector3 계산에 사용
    Vector3 tempVector3;
    //Vector2 계산에 사용
    Vector2 tempVector2 = new Vector2();
    //새총 발사에 사용되는 오브젝트 처리
    GameObject tempObj;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            tempVector3 = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            tempVector3.z = this.GetComponent<Transform>().position.z;
            print("마우스 입력 시 :" + tempVector3);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(FirePoint.position, tempVector3);
    }

    
}

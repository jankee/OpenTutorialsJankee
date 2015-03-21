using UnityEngine;
using System.Collections;

public class FarmTouchControl : MonoBehaviour {

    //마우스 클릭으로 입력된 좌표를 공간 좌표로 변환하는데 사용
    public Camera MainCamera;
    //발사할 게임 오브젝트
    public GameObject FireObj;
    //새총을 발사할 지점
    public Transform FirePoint;
    //새총을 발사 할 방향
    Vector3 fireDirection;
    //발사 속도
    public float FireSpeed;

    //발사 가능 여부판다
    bool enableAttack = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

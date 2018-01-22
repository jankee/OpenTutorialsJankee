using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //선택된 플레이어를 담기 위한 변수
    private Player selectPlayer;

    //이동할 플레이어를 담기 위한 변수
    private Player movePlayer;

    //마우스 움직임 확인
    private bool bMove = false;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //인풋 메소드
    public void HandleInput(Ray ray, RaycastHit hitInfo)
    {
        //마우스 눌러졌을 때
        if (Input.GetMouseButtonDown(0))
        {
            ///플레이들만 선택할수 있다
            ///기존 선택한 플레이어를 다시 선택을 했는가, 아닌가?
            ///플레이러를 드레그해서 무브를 하는지, 선택은 아니다.
            ///
        }
        else if (Input.GetMouseButton(0))
        {
            ///캐릭터를 선택해서 드레그를 하는지 체크
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ///드레그였는지 확인
            ///마우스를 up했을때 적이 있으면 타겟으로 설정
            ///선택된 플레이어이미 이동하고 있다면 코루틴을 꺼주고 지금 위치로 새롭게 무브 코루틴 시작
            ///다른 플레이어일때는 지금 위치로 무브 코루틴을 시작
            ///드레그가 아닐때는 캐릭터를 선택한다.
        }
    }

    public void OnRayCaset()
    {
        //마우스 위치에서 레이를 쏜다
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 255))
        {
            HandleInput(ray, hitInfo);
        }

        print("시작");
    }

    public void OnDragEnd()
    {
        print("끝");
    }
}
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

    //타겟 선택 시 생성되는 오브젝트
    [SerializeField]
    private GameObject mTarget;

    private GameObject tmp;

    private Coroutine moveRoutine;

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
            ///기존 선택한 플레이어를 다시 선택을 했는가
            ///플레이러를 드레그해서 무브를 하는지, 선택은 아니다.
            ///

            if (selectPlayer != null && hitInfo.transform.name == selectPlayer.name)
            {

            }

            //if (player != null && hitInfo.transform.name == player.name)
            //{
            //    tmp = Instantiate(mTarget, hitInfo.transform.position, Quaternion.identity);
            //}
            //else if (player != null && hitInfo.transform.name != player.name)
            //{
            //    player = hitInfo.transform.GetComponent<Player>();

            //    tmp = Instantiate(mTarget, hitInfo.transform.position, Quaternion.identity);
            //}
            //else
            //{
            //    //플레이어를 선택했을때
            //    if (hitInfo.transform.tag == "Player")
            //    {
            //        player = hitInfo.transform.GetComponent<Player>();

            //        if (!player.MyMove)
            //        {
            //            tmp = Instantiate(mTarget, hitInfo.transform.position, Quaternion.identity);
            //        }
            //    }
            //    else if (hitInfo.transform.tag == "Enemy")
            //    {
            //        print("Enemy");
            //    }
            //    else
            //    {
            //        if (moveRoutine != null)
            //        {
            //            StopCoroutine(moveRoutine);
            //        }

            //        if (player != null)
            //        {
            //            player = null;
            //        }
            //        print("None");
            //    }
            //}
        }
        else if (Input.GetMouseButton(0))
        {
            ///드래그 중에는 타겟이 마우스를 따라 다닌다.
            Vector3 vMovePos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

            tmp.transform.position = vMovePos;

            //print(hitInfo.point);

            Debug.DrawRay(ray.origin, hitInfo.point, Color.red);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ///선택한 플레이어가 이미 선택 된 플레이어인지 확인
            if (selectPlayer != null && hitInfo.transform.name == selectPlayer.name)
            {
                if (moveRoutine != null)
                {
                    StopCoroutine(moveRoutine);
                }

                moveRoutine = StartCoroutine(selectPlayer.Move(hitInfo.transform.position));

                Destroy(tmp.gameObject);

                print("선택 되어있는것이랑 같다");
            }
            else if (selectPlayer != null && hitInfo.transform.name != selectPlayer.name)
            {
                print("선택 되어있는거랑 다르다");

                moveRoutine = StartCoroutine(selectPlayer.Move(hitInfo.transform.position));

                Destroy(tmp.gameObject);
            }
            else
            {
                ///플레이어가 같다면 새로운 포인트로 이동
                ///에너미를 선택을 하면 공격
                //tmp.transform.position = hitInfo.transform.position;

                //if (moveRoutine != null)
                //{
                //    StopCoroutine(moveRoutine);
                //}

                //if (player != null)
                //{
                //    moveRoutine = StartCoroutine(player.Move(hitInfo.transform.position));
                //}

                //Destroy(tmp.gameObject);

                //bMove = false;
            }

            //if (!player.MyMove)
            //{
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
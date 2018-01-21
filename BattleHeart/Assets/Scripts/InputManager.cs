using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //선택된 플레이어를 담기 위한 변수
    private Player selectPlayer;

    //이동할 플레이어를 담기 위한 변수
    private Player movePlayer;

    private Vector3 startPos;

    private Vector3 endPos;

    //타겟 선택 시 생성되는 오브젝트
    [SerializeField]
    private GameObject mTarget;

    private GameObject tmp;

    private Coroutine moveRoutine;

    private bool mBool = false;

    // Use this for initialization
    void Start()
    {
        selectPlayer = null;
        movePlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        OnRayCaset();
    }

    public void HandleInput(Ray ray, RaycastHit hitInfo)
    {
        //마우스 눌러졌을 때
        if (Input.GetMouseButtonDown(0))
        {
            startPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
        }
        else if (Input.GetMouseButton(0))
        {

            float distance = Vector3.Distance(startPos, new Vector3(hitInfo.point.x, 0, hitInfo.point.z));

            if (distance > 1f)
            {
                //tmp가 없다면 
                if (tmp == null)
                {
                    tmp = Instantiate(mTarget, hitInfo.point, Quaternion.identity);
                }
                else
                {
                    // y축을 0으로 바꾸어준다 
                    Vector3 movePos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

                    Debug.DrawRay(ray.origin, movePos, Color.red);

                    tmp.transform.position = movePos;
                }
                mBool = true;
            }
            else
            {
                mBool = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (mBool)
            {
                if (selectPlayer == null)
                {
                    selectPlayer = hitInfo.transform.GetComponent<Player>();

                    Destroy(tmp.gameObject);

                    endPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

                    moveRoutine = StartCoroutine(selectPlayer.Move(endPos));
                    print("저장된 플레이어가 없다");
                }
                else
                {
                    if (selectPlayer.name != hitInfo.transform.name)
                    {
                        // movePlayer에 기존 플레이어를 넣는다
                        movePlayer = selectPlayer;

                        selectPlayer = hitInfo.transform.GetComponent<Player>();

                        print("같지 않은 플레이어 선택");
                        
                    }
                    else
                    {
                        movePlayer = null;
                        print("같은 플레이어");
                    }

                    endPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

                    StartCoroutine(selectPlayer.Move(endPos));

                    Destroy(tmp.gameObject);
                }
            }
            else
            {
                if (selectPlayer == null)
                {
                    selectPlayer = hitInfo.transform.GetComponent<Player>();

                    print("저장된 플레이어가 없다");
                }
                else
                {
                    if (selectPlayer.name != hitInfo.transform.name)
                    {
                        movePlayer = selectPlayer;

                        selectPlayer = hitInfo.transform.GetComponent<Player>();

                        print("같지 않은 플레이어 선택");
                    }
                    else
                    {
                        movePlayer = null;
                        print("같은 플레이어");
                    }
                }

            }

            //if (mBool)
            //{
            //    if (moveRoutine != null)
            //    {
            //        StopCoroutine(moveRoutine);
            //    }
            //    else
            //    {
            //        moveRoutine = StartCoroutine(selectPlayer.Move(hitInfo.transform.position));

            //        Destroy(tmp.gameObject);
            //    }
            //}
            //else
            //{
            //    selectPlayer = hitInfo.transform.GetComponent<Player>();

            //    print("선택");
            //}
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
    }
}

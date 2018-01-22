﻿using System.Collections;
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

    [SerializeField]
    private GameObject mSelect;

    private GameObject tmpSelect;

    private bool bDrag = false;

    // Use this for initialization
    private void Start()
    {
        selectPlayer = null;
        movePlayer = null;

        tmp = null;
        tmpSelect = null;
    }

    // Update is called once per frame
    private void Update()
    {
        OnRayCaset();
    }

    public void HandleInput(Ray ray, RaycastHit hitInfo)
    {
        //마우스 눌러졌을 때
        if (Input.GetMouseButtonDown(0))
        {
            if (hitInfo.transform.tag == "Player")
            {
                movePlayer = hitInfo.transform.GetComponent<Player>();

                //거리 측정을 위해 시작포인트를 알려 준다
                startPos = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);

                //OnDrawGizmos(startPos, Color.red);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            ///드레그인지 확인
            float distance = Vector3.Distance(startPos, new Vector3(hitInfo.point.x, 0, hitInfo.point.z));

            if (distance > 1f)
            {
                print("Drag");
                //tmp가 없다면
                if (tmp == null)
                {
                    tmp = Instantiate(mTarget, hitInfo.point, Quaternion.identity);
                }
                else
                {
                    //드레그
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

                    tmp.transform.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                }
                bDrag = true;
            }
            else
            {
                print("None Drag");
                bDrag = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (bDrag)
            {
                endPos = hitInfo.point;

                if (movePlayer.MyMoveRoutine != null)
                {
                    StopCoroutine(movePlayer.MyMoveRoutine);

                    movePlayer.MyMoveRoutine = null;
                }

                float rotate = Mathf.Atan2(endPos.z, endPos.x) * Mathf.Rad2Deg;

                print("오일러 각도 : " + rotate);

                movePlayer.MyMoveRoutine = StartCoroutine(movePlayer.MoveRoutine(hitInfo.point, rotate));

                //만약 적이라면
                if (hitInfo.transform.tag == "Enemy")
                {
                    //플레이어의 타겟에 에너미를 넘겨준다.
                    print("I finde Enemy");
                    movePlayer.MyTarget = hitInfo.transform.GetComponent<Enemy>();
                }

                Destroy(tmp.gameObject);
            }
            else
            {
                if (tmpSelect != null)
                {
                    print(tmpSelect.name);

                    Destroy(tmpSelect.gameObject);
                }

                //movePlayer를 selectPlayer에 전달
                selectPlayer = movePlayer;

                //커서를 생성하여 플레이어 자식으로 만듬
                tmpSelect = Instantiate(mSelect, selectPlayer.transform.position, Quaternion.identity);
                tmpSelect.transform.SetParent(selectPlayer.transform);
            }
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
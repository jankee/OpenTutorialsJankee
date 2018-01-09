using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;


    //----------------------------------------------
    public AnimationCurve curve;

    private Camera _mainCamera = null;
    private bool _mouseState;
    private GameObject target;

    private Vector3 originPos;

    private Vector3 movePos;

    private Ray ray;

    private Color originColor;

    [SerializeField]
    private GameObject testObj;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        print(LayerMask.GetMask("Clickable"));
        //=================================================================================
        if (Input.GetMouseButtonDown(0))
        {
            //빛에 맞은 충돌체
            RaycastHit hitInfo;

            //카메라에서 빛을 쏜다.
            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray.origin, ray.direction * 100, out hitInfo);

            print(hitInfo.transform.tag);

            if (hitInfo.transform.tag == "Ground")
            {

                if(target != null)
                {

                    target.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = originColor;
                    target = null;
                }
            }

            if (hitInfo.transform.tag == "Player")
            {
                _mouseState = true;

                //만약 타겟이 있는지 없는지
                if (target != null)
                {
                    //만약 선택된 타겟이 있다면 기존 컬러로 돌린다
                    if (target.name != hitInfo.transform.name)
                    {
                        var t = target.transform.Find("Cube");
                        var mr = t.GetComponent<MeshRenderer>();
                        mr.material.color = originColor;

                        target = hitInfo.collider.gameObject;
                        target.transform.Find("Cube").GetComponent<MeshRenderer>().material.color = Color.yellow;

                        //originPos에 위치를 저장
                        //originPos = target.transform.position;
                    }

                }
                else
                {
                    target = hitInfo.collider.gameObject;

                    Transform t = target.transform.Find("Cube");
                    MeshRenderer mr = t.GetComponent<MeshRenderer>();
                    //기존컬러를 저장
                    originColor = mr.material.color;
                    //선택 후 노란색으로 지정 
                    mr.material.color = Color.yellow;

                    //originPos에 위치를 저장
                    //originPos = target.transform.position;
                }

                //originPos에 위치를 저장
                originPos = target.transform.position;

            }
            else if (hitInfo.transform.tag == "Enemy")
            {
                //공격 할 것
            }

            
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;

            StopAllCoroutines();

            StartCoroutine(GetMoveObject(originPos, movePos, target));

        }

        if (_mouseState)
        {
            RaycastHit hit;

            ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray.origin, ray.direction * 100, out hit);
            
            movePos = hit.point;
        }
    }

    IEnumerator GetMoveObject(Vector3 origin, Vector3 move, GameObject target)
    {

        Vector3 originVec = new Vector3(origin.x, 0, origin.z);
        Vector3 moveVec = new Vector3(move.x, 0, move.z);

        float distance = Vector3.Distance(originVec, moveVec);
        float finishTime = 0;

        print(originVec +" : " + moveVec + " : " + distance);

        //거리가 0.5이상 떨어져 있다면 움직인다
        while (distance >= 0.5)
        {
            //캐릭터의 이동속도를 받아야 함
            finishTime += 0.5f * Time.deltaTime;

            var c = curve.Evaluate(finishTime);

            target.transform.position = Vector3.MoveTowards(originVec, moveVec, c);

            originVec = target.transform.position;

            distance = Vector3.Distance(originVec, moveVec);

            yield return null;

        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(ray.origin, ray.direction * 100);
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

        }
    }


}

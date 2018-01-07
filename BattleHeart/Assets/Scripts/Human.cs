using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject target;

    public Vector2 nowPos, prePos;
    public Vector3 movePos;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //target = GetClickedObject();

            StartCoroutine(MouseDrag());
        }

        if (Input.touchCount == 1)
        {
            TouchDragEnd();
        }
    }

    //유니 히트처리 부분, 레이를 사용한다
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        print("Ray : " + ray);

        //마우스 근처에 오브젝트가 있는지 확인
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            target = hit.collider.gameObject;
            print(target.name);
        }

        return target;
    }

    private void TouchDragEnd()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            print("Touch Begin!");
            prePos = touch.position - touch.deltaPosition;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            nowPos = touch.position - touch.deltaPosition;
            movePos = (Vector3)(prePos - nowPos) * speed;

            this.transform.Translate(movePos);

            prePos = touch.position - touch.deltaPosition;
        }
        else if (touch.phase == TouchPhase.Ended)
        {

        }
    }

    IEnumerator MouseDrag()
    {

        while (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            //Vector3 mouseDragPos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            print("Ray : " + ray);

            if (true == (Physics.Raycast(ray.origin, ray.direction * 100, out hit)))
            {
                this.transform.position = hit.collider.transform.position;

                print(hit.collider.transform.position);
            }

            //Vector3 worldObjectPos = Camera.main.ScreenToWorldPoint(mouseDragPos);


            yield return null;
        }
    }




    public void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}

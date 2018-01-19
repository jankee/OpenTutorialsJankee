using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //캐릭터 무브 스피드
    [SerializeField]
    private float mSpeed;

    private bool bMove = false;

    public bool MyMove
    {
        get
        {
            return bMove;
        }
    }

    // Use this for initialization
    private void Start()
    {
        bMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public IEnumerator Move(Vector3 moveEnd)
    {
        print("움직임");

        float distance = Vector3.Distance(this.transform.position, moveEnd);

        while (distance >= 0.5f)
        {
            //bMove = true;

            this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, mSpeed * Time.deltaTime);

            yield return null;

            distance = Vector3.Distance(this.transform.position, moveEnd);
        }

        //bMove = false;

        print("움직임 끝");
    }
}
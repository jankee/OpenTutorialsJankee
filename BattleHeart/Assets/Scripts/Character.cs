using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //캐릭터 무브 스피드
    [SerializeField]
    private float speed;

    protected Animator animator;

    private Rigidbody myRigidbody;

    // Use this for initialization
    protected virtual void Start()
    {
        animator = this.GetComponentInChildren<Animator>();

        myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public virtual IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 1);

        float distance = Vector3.Distance(this.transform.position, moveEnd);

        animator.SetBool("MOVE", true);

        print("움직임" + this.transform.localEulerAngles);
        print("오일러 각도 : " + rotate);

        if (315f <= rotate || rotate <= 45f)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            print("왼쪽 : " + rotate);
        }
        else if (45f <= rotate && rotate <= 135f)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
            print("아래 : " + rotate);
        }
        else if (135f <= rotate && rotate <= 225f)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
            print("오른쪽 : " + rotate);
        }
        else if (225f <= rotate && rotate <= 315f)
        {
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
            print("위쪽 : " + rotate);
        }

        while (distance >= 0.5f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, speed * Time.deltaTime);

            yield return null;

            distance = Vector3.Distance(this.transform.position, moveEnd);
        }
        animator.SetBool("MOVE", false);
    }

    public virtual void AnimateMovement()
    {
        var r = Mathf.Atan2(1f, 0f);
        var e = r * Mathf.Rad2Deg;
    }
}
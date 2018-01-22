using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //캐릭터 무브 스피드
    [SerializeField]
    private float speed;

    private Animator animator;

    // Use this for initialization
    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public virtual IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        float distance = Vector3.Distance(this.transform.position, moveEnd);

        print("움직임" + this.transform.localEulerAngles);

        if (-45f <= rotate && rotate <= 45f)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (46f <= rotate && rotate <= 135)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (136f <= rotate && rotate <= -135f)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (226f <= rotate && rotate <= 315f)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        while (distance >= 0.5f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, speed * Time.deltaTime);

            yield return null;

            distance = Vector3.Distance(this.transform.position, moveEnd);
        }
    }

    public virtual void AnimateMovement()
    {
        var r = Mathf.Atan2(1f, 0f);
        var e = r * Mathf.Rad2Deg;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //캐릭터 무브 스피드
    [SerializeField]
    private float speed;

    public Animator MyAnimator { get; set; }

    [SerializeField]
    private GameObject selectObj;

    private Rigidbody myRigidbody;

    public bool MyIsAttacking { get; set; }

    public Coroutine MyAttackRoutine { get; set; }

    public int MyExitIndex { get; set; }

    public bool isGround { get; set; }

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    private Stat health;

    public Stat MyHealth
    {
        get
        {
            return health;
        }
    }

    [SerializeField]
    private float initHealth;

    private bool isMoving;

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        MyHealth.Initalize(initHealth, initHealth);

        MyAnimator = this.GetComponentInChildren<Animator>();

        myRigidbody = this.GetComponent<Rigidbody>();

        MyIsAttacking = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    public virtual IEnumerator MoveRoutine(Vector3 moveEnd, float rotate)
    {
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 1);

        Rotation(rotate);

        if (MyAnimator != null)
        {
            MyAnimator.SetBool("MOVE", true);
        }

        if (/*지면과 캐릭터를 구분*/isGround)
        {
            while (Vector3.Distance(this.transform.position, moveEnd) >= 0f)
            {
                isMoving = true;

                this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, speed * Time.deltaTime);

                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(this.transform.position, moveEnd) >= 0.6f)
            {
                isMoving = true;

                this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, speed * Time.deltaTime);

                yield return null;
            }
        }

        isMoving = false;

        if (MyAnimator != null)
        {
            MyAnimator.SetBool("MOVE", false);
        }
    }

    //public virtual IEnumerator MoveRoutine(Vector3 moveEnd, float rotate, float fff)
    //{
    //    //animator.SetLayerWeight(0, 0);
    //    //animator.SetLayerWeight(1, 1);

    //    Rotation(rotate);

    //    if (MyAnimator != null)
    //    {
    //        MyAnimator.SetBool("MOVE", true);
    //    }

    //    //거리가 3유닛일때까지 진행
    //    while (Vector3.Distance(this.transform.position, moveEnd) >= 0.6f)
    //    {
    //        isMoving = true;

    //        this.transform.position = Vector3.MoveTowards(this.transform.position, moveEnd, speed * Time.deltaTime);

    //        yield return null;

    //        //distance = Vector3.Distance(this.transform.position, moveEnd);
    //    }

    //    isMoving = false;

    //    if (MyAnimator != null)
    //    {
    //        MyAnimator.SetBool("MOVE", false);
    //    }
    //}

    private void Rotation(float rotate)
    {
        if (315f <= rotate || rotate <= 45f)
        {
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
            MyExitIndex = 0;
            //print("왼쪽 : " + rotate);
        }
        else if (45f <= rotate && rotate <= 135f)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
            MyExitIndex = 0;
            //print("아래 : " + rotate);
        }
        else if (135f <= rotate && rotate <= 225f)
        {
            this.transform.localEulerAngles = new Vector3(0, 90, 0);
            MyExitIndex = 0;
            //print("오른쪽 : " + rotate);
        }
        else if (225f <= rotate && rotate <= 315f)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            MyExitIndex = 0;
            //print("위쪽 : " + rotate);
        }
    }

    public void HandleLayers()
    {
        if (MyIsAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");

            StopAttack();
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            //모든 레이어 값을 0 만든다
            MyAnimator.SetLayerWeight(i, 0);
        }
        //레이어를 활성화 한다
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        //코루틴이 아직 실행 중이면 꺼준다
        if (MyAttackRoutine != null)
        {
            StopCoroutine(MyAttackRoutine);
        }
        //어택 리셋
        MyIsAttacking = false;
        MyAnimator.SetBool("ATTACK", MyIsAttacking);
    }

    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;
        print("Damage");

        if (health.MyCurrentValue <= 0)
        {
            MyAnimator.SetLayerWeight(3, 1);

            MyAnimator.SetTrigger("Die");

            DisableTarget();

            //UIManager.Instance.HideTagetFrame(this.GetComponent<Enemy>());

            print("Die");
            //Die
        }
    }

    public virtual void ActivateTarget()
    {
        selectObj.SetActive(true);
    }

    public virtual void DisableTarget()
    {
        selectObj.SetActive(false);
    }
}
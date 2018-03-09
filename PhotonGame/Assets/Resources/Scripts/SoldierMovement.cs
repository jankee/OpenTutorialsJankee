using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : Photon.MonoBehaviour
{
    private CharacterController cc;

    private SoldierAnimation sAnim;

    private Vector3 moveDirection = Vector3.zero;

    private SoldierStat stat;

    public float _speed;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        sAnim = GetComponent<SoldierAnimation>();

        stat = GetComponent<SoldierStat>();
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.isMine && stat.state != SoldierStat.STATE.DEATH)
        {
            Move();

            Turn();
        }
    }

    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(h, 0f, v);

        float speed = _speed;

        if (h != 0 || v != 0)
        {
            sAnim.PlayAnim(SoldierStat.STATE.MOVE);
        }
        else
        {
            sAnim.PlayAnim(SoldierStat.STATE.IDLE);
        }

        if (h != 0 && v != 0)
        {
            float degree = Mathf.Cos(45f * Mathf.Deg2Rad);
            speed = speed * degree;
        }

        cc.SimpleMove(moveDirection * speed);
    }

    private void Turn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo, 100f, 1 << LayerMask.NameToLayer("Floor")))
        {
            Vector3 playerToMouseDir = hitInfo.point - transform.position;

            playerToMouseDir.y = 0f;

            //위치를 향한 회전값
            Quaternion qt = Quaternion.LookRotation(playerToMouseDir);

            transform.rotation = qt;
        }
    }
}
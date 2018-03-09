using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : Photon.MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //현재 캐릭터 오브젝트가 클라이언트 유저가 소유인지 체크
        if (photonView.isMine)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            photonView.transform.Translate(new Vector3(h, 0, v) * 5f * Time.deltaTime);
        }
    }
}
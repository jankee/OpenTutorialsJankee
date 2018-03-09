using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierHealth : Photon.MonoBehaviour
{
    public Image hpImage;

    private SoldierAnimation anim;

    public ParticleSystem bloodEffect;

    private SoldierStat state;

    private void Awake()
    {
        anim = GetComponent<SoldierAnimation>();

        state = GetComponent<SoldierStat>();
    }

    // Use this for initialization
    private void Start()
    {
        CloudLoadHP();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            // 총알 쏜 유저의 아이디
            int bulletPVID = other.GetComponentInChildren<Bullet>().ownerPVID;

            // 총알 파괴
            Destroy(other.gameObject);

            //방장이 판단을 하도록 한다
            if (PhotonNetwork.isMasterClient)
            {
                // 모든 클라이언트들에 현재 캐릭터들에게 데미지 감소를 요청
                photonView.RPC("TakeDamage", PhotonTargets.AllViaServer, bulletPVID);
            }
        }
    }

    [PunRPC]
    private void TakeDamage(int bulletPVID)
    {
        bloodEffect.Play();

        int HP = HPDown(20);

        if (HP <= 0)
        {
            //자신 컴포넌트의 비활성화
            GetComponent<CharacterController>().enabled = false;

            anim.PlayAnim(SoldierStat.STATE.DEATH);

            if (PhotonNetwork.isMasterClient)
            {
                //총알을 쏜 클라인언트가 생성한 캐릭터의 PhotonView ID를 가지고 현재 클라인언트안에서
                //해당 캐릭터의 PhotonView 아이디를 가진 캐릭터의 PhotonView의 찾아 참조를 리턴함
                PhotonView pv = PhotonView.Find(bulletPVID);

                //총알 소유주의 클라이언트의 점수를 증가 시켜줌
                pv.owner.AddScore(1);
            }
        }
    }

    //활용해봐야 하는 함수
    public int HPDown(int damage)
    {
        hpImage.fillAmount -= (damage * 0.01f);

        if (PhotonNetwork.isMasterClient)
        {
            CloudLoadHP();
        }

        return (int)(hpImage.fillAmount * 100);
    }

    public void CloudLoadHP()
    {
        if (photonView.owner.CustomProperties.ContainsKey("HP"))
        {
            hpImage.fillAmount = (float)photonView.owner.CustomProperties["HP"];

            int hp = (int)(hpImage.fillAmount * 100f);

            //자신 컴포넌트의 비활성화
            GetComponent<CharacterController>().enabled = false;

            anim.PlayAnim(SoldierStat.STATE.DEATH);

            if (hp <= 0)
            {
                anim.PlayAnim(SoldierStat.STATE.DEATH);
            }
        }
    }

    public void CloudSaveHP()
    {
        //포톤 해쉬테이블 생성
        ExitGames.Client.Photon.Hashtable hpInfo = new ExitGames.Client.Photon.Hashtable();

        //체력 포톤 해쉬테이블에 체력값을 저장
        hpInfo.Add("HP", hpImage.fillAmount);

        photonView.owner.SetCustomProperties(hpInfo);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierShot : Photon.MonoBehaviour
{
    private SoldierStat stat;

    public float shotDelayTime;
    private float timer;

    public GameObject bulletPrefab;

    public Transform shotPos;

    public float shotPower;

    private void Awake()
    {
        stat = GetComponent<SoldierStat>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (photonView.isMine && stat.state != SoldierStat.STATE.DEATH)
        {
            timer += Time.deltaTime;

            if (Input.GetButtonDown("Fire1") && timer >= shotDelayTime)
            {
                Shot(shotPos.position, shotPos.forward, transform.rotation, photonView.viewID);

                ///[실행 범위]
                ///PhotonTargets.All : 모두에게 (직접)
                ///PhotonTargets.MasterClient : 방장에게만
                ///PhotonTargets.Others : 나를 제외한
                ///PhotonTargets.AllViaServer : 중계 서버를 통해 동등한 조건으로 모두에게

                photonView.RPC("Shot", PhotonTargets.AllViaServer, shotPos.position, shotPos.forward, transform.rotation, photonView.viewID);
            }
        }
    }

    [PunRPC]
    public void Shot(Vector3 pos, Vector3 forward, Quaternion qt, int pvId)
    {
        timer = 0;

        GameObject bullet = Instantiate(bulletPrefab, pos, qt);

        bullet.GetComponentInChildren<Bullet>().ownerPVID = pvId;

        bullet.GetComponentInChildren<Rigidbody>().velocity = forward * shotPower;

        Destroy(bullet, 0.8f);
    }
}
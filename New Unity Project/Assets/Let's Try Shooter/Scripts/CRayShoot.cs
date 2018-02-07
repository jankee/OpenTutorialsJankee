using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRayShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate;
    public float hitForce;
    public float weaponRange;
    public Transform gunEnd;

    [SerializeField]
    private Camera fpsCam;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;

    private float nextFire;

    // Use this for initialization
    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        //fpsCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //발포시간 지연
            nextFire = Time.time + fireRate;

            Shoot();
        }
    }

    public void Shoot()
    {
        //이펙트 효과
        StartCoroutine(ShotEffectCoroutine());

        //카메라 화면의 정가운데 뷰포트 위치를 월드 좌표를 구함
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        //충돌 정보 저장용 구조체 생성
        RaycastHit hit;

        //
        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            //레이저 표시의 끝위치 (인덱스:1)의 위치를 설정함
            //위치는 충돌된 위치로
            laserLine.SetPosition(1, hit.point);

            CShootableBox box = hit.collider.GetComponent<CShootableBox>();

            if (box != null)
            {
                if (box.boxState == CShootableBox.BOX_STATE.NON_SHOOTABLE)
                {
                    return;
                }

                //
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    private IEnumerator ShotEffectCoroutine()
    {
        //총소리 재생
        gunAudio.Play();

        //레이저 활성화
        laserLine.enabled = true;

        //지연 시간
        yield return shotDuration;

        //레이저 비활성화
        laserLine.enabled = false;
    }
}
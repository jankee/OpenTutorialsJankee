using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShootableBox : MonoBehaviour
{
    public int currentHealth = 3;

    private bool isShooting = false;

    public enum BOX_STATE
    {
        SHOOTABLE,
        NON_SHOOTABLE,
        DUMMY,
    };

    public BOX_STATE boxState;

    public Material[] colorMaterials;

    public MeshRenderer _renderer;

    public Coroutine chageRoutine = null;

    // Use this for initialization
    private void Start()
    {
        float t = Random.Range(1f, 2.5f);

        StartCoroutine(StateChangeCoroutine(t));
    }

    // Update is called once per frame
    private IEnumerator StateChangeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        boxState = (BOX_STATE)Random.Range(0, 2);

        //박스 상태에 색상 변경
        _renderer.material = colorMaterials[(int)boxState];

        float t = Random.Range(1f, 2.5f);

        //이전 코루틴 중단
        if (chageRoutine != null)
        {
            StopCoroutine(chageRoutine);
        }

        //다시 자신 코루틴을 실행
        chageRoutine = StartCoroutine(StateChangeCoroutine(t));
    }

    public void AimTrigger()
    {
    }
}
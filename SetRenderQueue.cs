using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetRenderQueue : MonoBehaviour 
{
    public int _renderQueue = 3000; // 수정을 할 렌더큐값, NGUI 기준 기본 3000, 즉 NGUI 에서 사용하려면 3000 이상을 넣어줘야합니다.
    private MeshRenderer[] mMat;
    private ParticleSystemRenderer[] pMat;


    public void Start()
    {
        mMat = this.GetComponentsInChildren<MeshRenderer>();
        pMat = this.GetComponentsInChildren<ParticleSystemRenderer>();

        print(mMat.Length + " " + pMat.Length);

        for (int i = 0; i < mMat.Length; i++)
        {
            mMat[i].sharedMaterial.renderQueue = _renderQueue;

            print("RenderQueue :" + mMat[i].sharedMaterial.renderQueue);
        }

        for (int i = 0; i < pMat.Length; i++)
        {
            pMat[i].material.renderQueue = _renderQueue;
        }
    } 
}

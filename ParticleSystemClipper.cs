﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(UIPanel))]
public class ParticleSystemClipper : MonoBehaviour 
{
    const string ShaderName = "FXMaker/Additive Area Clip";
    const float ClipInterval = 0.5f;

    UIPanel m_targetPanel;
    Shader p_shader;

	// Use this for initialization
	void Start () 
    {
        m_targetPanel = this.GetComponent<UIPanel>();

        if (m_targetPanel == null)
        {
            throw new ArgumentNullException("Can't find the right UIPanel");
        }
        if (m_targetPanel.clipping != UIDrawCall.Clipping.SoftClip)
        {
            throw new ArgumentNullException("Don't need to clip");
        }

        p_shader = Shader.Find(ShaderName);

        if (!IsInvoking("Clip"))
        {
            InvokeRepeating("Clip", 0, ClipInterval);
        }
	}
	
    Vector4 CalcClipArea()
    {
        var clipRegion = m_targetPanel.finalClipRegion;

        print(m_targetPanel.finalClipRegion);

        Vector4 nguiArea = new Vector4()
        {
            x = clipRegion.x - clipRegion.z / 2,
            y = clipRegion.y - clipRegion.w / 2,
            z = clipRegion.z + clipRegion.z / 2,
            w = clipRegion.y + clipRegion.w / 2
        };

        var uiRoot = GameObject.FindObjectOfType<UIRoot>();
        var pos = m_targetPanel.transform.position - uiRoot.transform.position;

        float h = 2;
        float temp = h / uiRoot.manualHeight;

        return new Vector4()
        {
            x = pos.x + nguiArea.x * temp,
            y = pos.y + nguiArea.y * temp,
            z = pos.x + nguiArea.z * temp,
            w = pos.y + nguiArea.w * temp

        };
    }

    void Clip()
    {
        Vector4 clipArea = CalcClipArea();

        var particleSystems = this.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < particleSystems.Length; i++)
        {
            var ps = particleSystems[i];

            var mat = ps.GetComponent<ParticleSystemRenderer>().material;

            if (mat.shader.name != ShaderName)
            {
                mat.shader = p_shader;
            }

            mat.SetVector("_Area", clipArea);
        }

        var meshRenderer = this.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < meshRenderer.Length; i++)
        {
            var ms = meshRenderer[i];

            var mat = ms.GetComponent<MeshRenderer>().material;

            if (mat.shader.name != ShaderName)
            {
                mat.shader = p_shader;
            }

            mat.SetVector("_Area", clipArea);
        }
    }

    void OnDestroy()
    {
        CancelInvoke("Clip");
    }
}

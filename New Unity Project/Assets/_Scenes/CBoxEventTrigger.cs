using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBoxEventTrigger : MonoBehaviour
{
    public Material[] colorMaterials;

    public MeshRenderer _renderer;

    [SerializeField]
    private Text _text;

    // Use this for initialization
    private void Start()
    {
        _renderer.material = colorMaterials[0];
    }

    //시선이 박스를 가리킴
    public void OnPointerEnterEvent()
    {
        StopCoroutine(ColorChangeCoroutine());

        StartCoroutine(ColorChangeCoroutine());
    }

    private IEnumerator ColorChangeCoroutine()
    {
        yield return new WaitForSeconds(0.25f);

        _renderer.material = colorMaterials[1];
    }

    //시선이 떠남
    public void OnPointerExitEvent()
    {
        _renderer.material = colorMaterials[0];
    }

    public void OnButtonEnterClick()
    {
        //_text.enabled = _text.enabled ? false : true;
        _text.enabled = !_text.enabled;
    }

    public void OnButtonExit()
    {
    }
}
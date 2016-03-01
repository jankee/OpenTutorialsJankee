using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void ButtonClickText()
    {
        HiveManager.checkButton = true;

        transform.parent.parent.GetComponentInChildren<Text>().text = GetComponentInChildren<Text>().text; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!HiveManager.checkButton)
        {
            transform.parent.parent.GetComponentInChildren<Text>().text = GetComponentInChildren<Text>().text;    
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!HiveManager.checkButton)
        {
            transform.parent.parent.GetComponentInChildren<Text>().text = "Please Select!";    
        }
    }
}

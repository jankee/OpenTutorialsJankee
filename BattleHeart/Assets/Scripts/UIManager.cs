using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3;

    // Use this for initialization
    private void Start()
    {
        //키바인드,
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
    }

    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }

    public void ShowTatgetFrame(Enemy target)
    {
        print(target.transform.name);
        target.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    public void HideTagetFrame(Enemy target)
    {
        target.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}
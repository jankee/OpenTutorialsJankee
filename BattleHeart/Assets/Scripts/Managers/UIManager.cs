using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    private Stat healthStat;

    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3;

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private CanvasGroup keybindMenu;

    private GameObject[] keybindButtons;

    public void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    // Use this for initialization
    void Start()
    {
        healthStat = targetFrame.GetComponentInChildren<Stat>();

        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        else if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        else if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
    }

    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }

    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        target.healthChanged += new HealthChanged(UpdateTatgetFrame);

        target.characterRemoved += new CharacterRemove(HideTargetFrame);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTatgetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }

    public void OpenCloseMenu()
    {
        keybindMenu.alpha = keybindMenu.alpha > 0 ? 0 : 1;

        keybindMenu.blocksRaycasts = keybindMenu.blocksRaycasts == true ? false : true;

        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponent<Text>();
        tmp.text = code.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyBinedManager : MonoBehaviour
{
    private static KeyBinedManager instance;

    public static KeyBinedManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBinedManager>();
            }

            return instance;
        }
    }

    public Dictionary<string, KeyCode> keybinds { get; set; }

    public Dictionary<string, KeyCode> actionBinds { get; set; }

    string bindName;

    // Use this for initialization
    void Start()
    {
        keybinds = new Dictionary<string, KeyCode>();
        actionBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.W);
        BindKey("LEFT", KeyCode.A);
        BindKey("RIGHT", KeyCode.D);
        BindKey("DOWN", KeyCode.S);

        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictinary = keybinds;

        if (key.Contains("ACT"))
        {
            currentDictinary = actionBinds;
        }

        if (!currentDictinary.ContainsKey(key))
        {
            currentDictinary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictinary.ContainsValue(keyBind))
        {
            string myKey = currentDictinary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictinary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictinary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    public void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }
}

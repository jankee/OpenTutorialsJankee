using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinedManager : MonoBehaviour
{
    private KeyBinedManager instance;

    public KeyBinedManager MyInstance
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

    // Use this for initialization
    void Start()
    {
        keybinds = new Dictionary<string, KeyCode>();
        actionBinds = new Dictionary<string, KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictinary = keybinds;

        if (key.Contains("ACT"))
        {
            currentDictinary = actionBinds;
        }

        if (!currentDictinary.ContainsValue(keyBind))
        {
            currentDictinary.Add(key, keyBind);
        }
    }
}

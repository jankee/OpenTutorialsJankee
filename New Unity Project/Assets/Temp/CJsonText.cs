using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

[Serializable]
public class Weapon
{
    public string sword = "빛나는 검";
    public string gun = "따발 총";
    public string bow = "";
}

public class Monster
{
    public string name = "오우거";
    public int level = 5;
    public int hp = 100;
    public Weapon weapons = new Weapon();
}

public class CJsonText : MonoBehaviour
{
    private Monster monster = new Monster();

    // Use this for initialization
    private void Start()
    {
        string jsonData = JsonUtility.ToJson(monster);

        print(jsonData);

        print(monster.name);

        Dictionary<string, object> monsterDic = new Dictionary<string, object>();

        monsterDic["name"] = "오우거";
        monsterDic["level"] = "5";
        monsterDic["hp"] = "100";
        monsterDic["weapons"] = new Dictionary<string, object>();
        Dictionary<string, object> weapons = monsterDic["weapons"] as Dictionary<string, object>;

        weapons["sword"] = "빛나는 검";
        weapons["gun"] = "따발 총";
        weapons["bow"] = "";

        jsonData = MiniJSON.jsonEncode(monsterDic);
        print(jsonData);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
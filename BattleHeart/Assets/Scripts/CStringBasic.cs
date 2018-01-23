using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStringBasic : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        bool bResurlt = false;
        int iResurlt = 0;

        string str1 = "abc";
        string str2 = "def";
        string temp = "abc";

        print("str : " + str1 + ", " + "str2 : " + str2);

        iResurlt = string.Compare(str1, temp);
        print("string.Compare(str1, temp) : " + iResurlt);

        iResurlt = str1.CompareTo(str2);
        print("str1.CompareTo(str2) : " + iResurlt);

        iResurlt = str2.CompareTo(str1);
        print("str2.CompareTo(str1) : " + iResurlt);

        bResurlt = str2.Contains("ef");
        print("str2.Contains(\"ef\") : " + iResurlt);

        string str3 = "Hello Unity!";
        iResurlt = str3.IndexOf("Unity");
        print("str3.IndexOf(\"Unity\") : " + iResurlt);

        iResurlt = str3.IndexOf("!");
        str3 = str3.Insert(iResurlt, "5");
        print("str3.Insert(iResurlt, \"5\") : " + str3);

        string sResult = str3.Remove(5, 7);
        print("string sResult = str3.Remove(5, 6) : " + sResult);

        string str5 = "______ Unity!!";
        sResult = str5.Replace("______", "Hello");
        print("str5.Replace(\"______\", \"Hello\") : " + sResult);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
using UnityEngine;
using System.Collections;
using System;

[Serializable]

public class Book : MonoBehaviour 
{

    public Book()
    {

    }

    //인스턴스 변수
    public string Title
    {
        get;
        set;
    }

    public string Auther
    {
        get;
        set;
    }

    public string Year
    {
        get;
        set;
    }

    //생성자 constructor
    public Book(string _title, string _auther, string _year)
    {
        Title = _title;
        Auther = _auther;
        Year = _year;

    }

    public string _ToString()
    {
        return Title + "\n" + Auther + "\n" + Year;
    }
}

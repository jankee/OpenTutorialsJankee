using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour 
{
    public static int numberOfBooks;

    //인스턴스 변수
    private string title;
    private string auther;
    private string year;

    //생성자 constructor
    public Book(string _title, string _auther, string _year)
    {
        title = _title;
        auther = _auther;
        year = _year;

        numberOfBooks++;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuther()
    {
        return auther;
    }

    public string GetYear()
    {
        return year;
    }

    public void SetTitle(string newTitle)
    {
        title = newTitle;
    }

    public string _ToString()
    {
        return title + "\n" + auther + "\n" + year;
    }
}

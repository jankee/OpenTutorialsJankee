using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class MainClass : MonoBehaviour 
{
    public static Text testText;

    public Text TestText;

    private static MainClass instance;

    public static MainClass Instance
    {
        get 
        {
            if (instance == null)
            {
                GameObject.Find("GameObject");
            }
            return MainClass.instance; 
        }
    }

    BookManager manager;

    public static int mainI = 0;

    public InputField InputFieldNum;

    private static InputField inputFieldText;

    public InputField InputFieldText;

    public string title = "";
    public string auther = "";
    public string year = "";

    public void Awake()
    {
        testText = TestText;

        inputFieldText = InputFieldText;
    }



	// Use this for initialization
	void Start () 
    {
         manager = new BookManager();

         testText.text = "Hi";

         manager.GetMenuChoice();
	}
	
    public void IntInput()
    {
        manager.Menu(Convert.ToInt32(InputFieldNum.text));

        testText.text = "Title";
    }

    public void TextInput()
    {
        manager.AddBook(InputFieldText.text.ToString());
    }

	// Update is called once per frame
	void Update () 
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TextEmpty();
        //}
	}

    public static void TextEmpty()
    {
        inputFieldText.text = string.Empty;
    }

    //IEnumerator TextShow(int i)
    //{
    //    //testText.text = manager.books[i]._ToString();
    //    yield return new WaitForSeconds(1); 
    //}
}

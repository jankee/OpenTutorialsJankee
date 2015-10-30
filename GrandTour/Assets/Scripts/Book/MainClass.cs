using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainClass : MonoBehaviour 
{
    public Text testText;

    public void Awake()
    {
        
    }



	// Use this for initialization
	void Start () 
    {
        Book b1 = new Book("Load of rings", "JRR tolken", 1950);

        testText.text = b1._ToString();

        print(b1._ToString());

        print(b1.GetYear());

        Debug.Log(b1._ToString());

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

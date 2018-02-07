using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class CHTTPNet : MonoBehaviour
{
    public InputField numField1;

    public InputField numField2;

    private float num1;

    private float num2;

    private int cal;

    private string calculator;

    public Text text1;

    public Text text2;

    public Text text3;

    public Text text4;

    // Use this for initialization
    private void Start()
    {
        //StartCoroutine(GetTest());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Calculator(int cal)
    {
        if (numField1.text != string.Empty && numField1.text != string.Empty)
        {
            num1 = float.Parse(numField1.text);
            num2 = float.Parse(numField2.text);
            this.cal = cal;

            switch (this.cal)
            {
                case 1:
                    calculator = "+";
                    break;

                case 2:
                    calculator = "-";
                    break;

                case 3:
                    calculator = "*";
                    break;

                case 4:
                    calculator = "/";
                    break;
            }

            StartCoroutine(GetCal(num1, num2, cal));
        }
    }

    private IEnumerator GetCal(float num1, float num2, int cal)
    {
        string url = "http://127.0.0.1/get_http.php";

        string getParamUrl = url + "?val1=" + num1 + "&val2=" + num2 + "&val3=" + cal;

        WWW www = new WWW(getParamUrl);

        yield return www;

        //예외 처리 부분
        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);

            yield break;
        }

        print(www.text);

        //딕셔너리로 받기
        Dictionary<string, object> responseData = (Dictionary<string, object>)
            MiniJSON.jsonDecode(www.text.Trim());

        text1.text = responseData["val1"].ToString();
        text2.text = calculator;
        text3.text = responseData["val2"].ToString();
        text4.text = responseData["data"].ToString();
    }

    private IEnumerator GetTest()
    {
        string url = "http://127.0.0.1/get_http.php";

        string getParamUrl = url + "?val1=10&val2=40";

        WWW www = new WWW(getParamUrl);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);

            yield break;
        }

        print(www.text);

        Dictionary<string, object> responseData = (Dictionary<string, object>)
            MiniJSON.jsonDecode(www.text.Trim());

        print("val1 => " + responseData["val1"]);
        print("val2 => " + responseData["val2"]);
        print("data => " + responseData["data"]);
    }
}
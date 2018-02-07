using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameData;

public class CLogin : MonoBehaviour
{
    public Button goLoginButton;

    public InputField LoginInputField;

    public Text msgField;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    public void LoginOn()
    {
        if (LoginInputField.text != null)
        {
            StartCoroutine(LoginOnRoutine());
        }
    }

    private IEnumerator LoginOnRoutine()
    {
        string nick_name = LoginInputField.text.Trim();

        string url = "http://127.0.0.1/insertselect.php";

        WWWForm form = new WWWForm();

        form.AddField("nick_name", nick_name);

        WWW www = new WWW(url, form);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            print("서버 통신 오류 발생");

            yield break;
        }

        print(www.text.Trim());

        Dictionary<string, object> responseData =
            MiniJSON.jsonDecode(www.text.Trim()) as Dictionary<string, object>;

        print(responseData["RESULT"].ToString());

        Dictionary<string, object> user_info =
            responseData["USER_INFO"] as Dictionary<string, object>;

        msgField.text = responseData["USER_INFO"].ToString();

        PlayerPrefs.SetString("USER_NICK", user_info["nick_name"].ToString());
        PlayerPrefs.SetString("TOTAL_COUNT", user_info["total_click_count"].ToString());
        PlayerPrefs.SetString("BEST_COUT", user_info["best_click_count"].ToString());
        //PlayerPrefs.SetInt("CHARA_SELECT", int.Parse(user_info["charac"]));

        msgField.text = PlayerPrefs.GetString("USER_NICK") + " : "
            + PlayerPrefs.GetString("TOTAL_COUNT") + " : "
            + PlayerPrefs.GetString("BEST_COUNT");

        PlayerPrefs.Save();

        //SceneManager.LoadScene("LoginOn");

        //if (result.Equals(nick_name + " IS ALLRADY"))
        //{
        //}
        //else
        //{
        //    msgField.text = responseData["RESULT"].ToString();

        //    pl
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;
using UnityEngine.SceneManagement;

public class CAcountManager : MonoBehaviour
{
    public InputField idInputField;

    public InputField pwInputField;

    public Text msgField;

    private void Update()
    {
        //if (idInputField.isFocused || pwInputField.isFocused)
        //{
        //    msgField.text = "";
        //}
    }

    // Use this for initialization
    public void OnLoginButton()
    {
        StartCoroutine(LoginNetCoroutine());
    }

    // Update is called once per frame
    public IEnumerator LoginNetCoroutine()
    {
        string url = "http://10.1.43.124/login.php";

        //포스트 통신 POST
        WWWForm form = new WWWForm();

        form.AddField("user_id", idInputField.text.Trim());
        form.AddField("user_pw", pwInputField.text.Trim());

        WWW www = new WWW(url, form);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            print("서버 통신 오류 발생");

            msgField.text = "서버 통신 오류 발생";

            yield break;
        }

        Dictionary<string, object> responseData = MiniJSON.jsonDecode(www.text.Trim()) as Dictionary<string, object>;

        string result = responseData["result"].ToString();

        if (result.Equals("LOGIN_FAIL"))
        {
            msgField.text = responseData["msg"].ToString();
            yield break;
        }

        Dictionary<string, object> userInfo = responseData["user_info"] as Dictionary<string, object>;

        msgField.text = "로그인 성공";

        PlayerPrefs.SetString("USER_ID", userInfo["id"].ToString());
        PlayerPrefs.SetString("TYPE", userInfo["type"].ToString());
        PlayerPrefs.SetString("SCORE", userInfo["score"].ToString());
        PlayerPrefs.Save();

        SceneManager.LoadScene("LoginOn");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameData;

public class UIManager : MonoBehaviour
{
    public InputField _nickNameInputField;

    [SerializeField]
    private GameObject _msgBox;

    public Text _msgText;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    public void OnNickNameEditEnterEvent()
    {
        if (string.IsNullOrEmpty(_nickNameInputField.text.Trim()))
        {
            _msgText.text = "You have not entered a nickname.";
            return;
        }
        StartCoroutine(LoadAccountInfoRoutine());
    }

    private IEnumerator LoadAccountInfoRoutine()
    {
        string url = "http://13.125.175.104/ci/index.php/gamecontroller/account_ctrl/?nick_name=";

        string nick_name = _nickNameInputField.text.Trim();

        url = url + nick_name;

        //form.AddField("nick_name", nick_name);

        //통신 요청
        WWW www = new WWW(url);
        //통신 요청 대기
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Response Data : " + www.text);

            Dictionary<string, object> responseData =
                MiniJSON.jsonDecode(www.text.Trim()) as Dictionary<string, object>;

            Dictionary<string, object> userInfo =
                responseData["USER_INFO"] as Dictionary<string, object>;

            string result = responseData["RESULT"].ToString();

            if (result.Equals("LOAD_SUCCESS"))
            {
                string saveData = MiniJSON.jsonEncode(userInfo);

                PlayerPrefs.SetString("USER_INFO", saveData);
                PlayerPrefs.Save();

                _msgBox.SetActive(true);
                _msgText.text = "Joins User!";

                yield return new WaitForSeconds(1.5f);

                _msgBox.SetActive(false);
                SceneManager.LoadScene("Main");
            }
            else if (result.Equals("INSERT_SUCCESS"))
            {
                string saveData = MiniJSON.jsonEncode(userInfo);

                PlayerPrefs.SetString("USER_INFO", saveData);
                PlayerPrefs.Save();

                _msgBox.SetActive(true);
                _msgText.text = "Create new User!";

                yield return new WaitForSeconds(1.5f);

                _msgBox.SetActive(false);
                SceneManager.LoadScene("Main");
            }
        }
        else
        {
            _msgText.text = "User infomation save fail.";
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameData;

public class GameManager : MonoBehaviour
{
    public Text _nickNameText;
    public Text _bestClickCountText;
    public Text _nowClickCountText;
    public Image _timerProgress;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Sprite[] portraySprites;

    [SerializeField]
    private Image characterSprite;

    [SerializeField]
    private Image portrayrSprite;

    private int totalCount = 0;

    // Use this for initialization
    private void Start()
    {
        string userInfoString = PlayerPrefs.GetString("USER_INFO", "");
        print(userInfoString);

        Dictionary<string, object> userInfo =
            MiniJSON.jsonDecode(userInfoString) as Dictionary<string, object>;

        _nickNameText.text = userInfo["nick_name"].ToString();

        totalCount = int.Parse(userInfo["total_click_count"].ToString());

        characterSprite.sprite = sprites[int.Parse(userInfo["charac_Select"].ToString())];

        portrayrSprite.sprite = portraySprites[int.Parse(userInfo["charac_Select"].ToString())];

        _bestClickCountText.text = userInfo["best_click_count"].ToString();

        StartCoroutine(TimerRoutine());
    }

    // Update is called once per frame
    public void OnClickerButton()
    {
        int clickCount = int.Parse(_nowClickCountText.text);
        int bestClickCount = int.Parse(_bestClickCountText.text);

        clickCount++;

        if (clickCount > bestClickCount)
        {
            _bestClickCountText.text = clickCount.ToString();
        }

        _nowClickCountText.text = clickCount.ToString();
    }

    private IEnumerator TimerRoutine()
    {
        float progressValue = Time.deltaTime;

        while (_timerProgress.fillAmount > 0f)
        {
            yield return new WaitForSeconds(progressValue * 10);

            _timerProgress.fillAmount -= progressValue;
        }

        Button offButton = GameObject.FindObjectOfType<Button>();

        offButton.enabled = false;
        offButton.GetComponent<Image>().color = Color.gray;
        offButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;

        string url = "http://127.0.0.1/ci/index.php/gamecontroller/update_ctrl";

        string nick_name = _nickNameText.text.Trim();

        totalCount += int.Parse(_nowClickCountText.text.Trim());

        int bestCount = int.Parse(_bestClickCountText.text.Trim());

        WWWForm form = new WWWForm();
        print("NickName : " + nick_name + " : " + totalCount + " : " + bestCount);

        form.AddField("nick_name", nick_name);
        form.AddField("total_click_count", totalCount);
        form.AddField("best_click_count", bestCount);

        print("NickName : " + nick_name + " : " + totalCount + " : " + bestCount);

        WWW www = new WWW(url, form);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print(www.text.Trim());

            Dictionary<string, object> responseData =
                MiniJSON.jsonDecode(www.text.Trim()) as Dictionary<string, object>;

            print(responseData["RESULT"].ToString());

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("Rank");
        }
    }
}
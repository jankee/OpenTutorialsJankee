using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameData;

public class RankManager : MonoBehaviour
{
    public GameObject rankInfoPanel;

    [SerializeField]
    private Transform contentTr;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(LoadRankRoutine());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Account");
    }

    // Update is called once per frame
    private IEnumerator LoadRankRoutine()
    {
        string url = "http://127.0.0.1/select_order_by.php";

        WWW www = new WWW(url);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Dictionary<string, object> rankData =
                MiniJSON.jsonDecode(www.text.Trim()) as Dictionary<string, object>;

            string result = rankData["RESULT"].ToString();

            if (result.Equals("ORDERBY_SUCCESS"))
            {
                List<object> rankList = rankData["USER_INFO"] as List<object>;

                Transform content = GameObject.Find("Content").transform;

                float posY = 0f;

                for (int i = 0; i < rankList.Count; i++)
                {
                    Dictionary<string, object> _data =
                        rankList[i] as Dictionary<string, object>;

                    //GameObject row = Instantiate(rankInfoPanel);

                    GameObject _row = Instantiate(rankInfoPanel, Vector3.zero, Quaternion.identity);

                    _row.transform.SetParent(contentTr);

                    _row.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, posY);
                    _row.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                    posY = posY - 40f;

                    print(_row.GetComponent<RectTransform>().localPosition);

                    //row.transform.SetParent(content);
                    //row.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = _data["nick_name"].ToString();
                    //row.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = _data["best_click_count"].ToString();

                    //row.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                    //print((i + 1).ToString() + "등 유저 아이디 : " + _data["nick_name"]);
                }

                RectTransform rt = contentTr.GetComponent<RectTransform>();

                rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Abs(posY));
            }
        }
    }
}
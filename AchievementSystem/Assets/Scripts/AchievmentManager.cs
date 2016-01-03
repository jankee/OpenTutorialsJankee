using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievmentManager : MonoBehaviour 
{
    public GameObject achievmentPrefab;

    public Sprite[] sprites;

    private AchievmentButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievmentMenu;

    public GameObject visualAchievment;

    public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>();

	// Use this for initialization
	void Start () 
    {
        //Example
        //achievments.Add("RunAchievment", new Achievment("Run", "He ran", 10, 0, this.gameObject));

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievmentButton>();
        CreateAchievment("Genaral", "Press W", "Press W to unlock this achievment", 5, 0);

        foreach (GameObject achievment in GameObject.FindGameObjectsWithTag("AchievmentList"))
        {
            achievment.SetActive(false);
        }

        activeButton.Click();

        achievmentMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            achievmentMenu.SetActive(!achievmentMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievment("Press W");
        }
	}

    public void EarnAchievment(string title)
    {
        if (achievments[title].EarnAchievment())
        {
            //DO SOMETHING AWESOME!
            GameObject achievment = (GameObject)Instantiate(visualAchievment);
            SetAchievmentInfo("EarnCanvas", achievment, title);

            StartCoroutine(HideAchievment(achievment));
        }
    }

    public IEnumerator HideAchievment(GameObject achiement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achiement);
    }

    public void CreateAchievment(string parent, string title, string description, int points, int spriteIndex)
    {
        GameObject achievment = (GameObject)Instantiate(achievmentPrefab);

        Achievment newAchievment = new Achievment(name, description, points, spriteIndex, achievment);

        achievments.Add(title, newAchievment);

        SetAchievmentInfo(parent, achievment, title);
    }

    public void SetAchievmentInfo(string parent, GameObject achievment, string title)
    {
        achievment.transform.SetParent(GameObject.Find(parent).transform);
        achievment.transform.localScale = new Vector3(1, 1, 1);
        achievment.transform.GetChild(0).GetComponent<Text>().text = title;
        achievment.transform.GetChild(1).GetComponent<Text>().text = achievments[title].Description;
        achievment.transform.GetChild(2).GetComponent<Text>().text = achievments[title].Point.ToString();
        achievment.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievments[title].SpriteIndex];
    }

    public void ChageCatagory(Button button)
    {
        AchievmentButton achievmentButton = button.GetComponent<AchievmentButton>();

        scrollRect.content = achievmentButton.achievmentList.GetComponent<RectTransform>();

        achievmentButton.Click();
        activeButton.Click();

        activeButton = achievmentButton;
    }
}
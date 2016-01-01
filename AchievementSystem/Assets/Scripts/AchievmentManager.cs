using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievmentManager : MonoBehaviour 
{
    public GameObject achievmentPrefab;

    public Sprite[] sprites;

    private AchievmentButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievmentMenu;

	// Use this for initialization
	void Start () 
    {
        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievmentButton>();

        CreateAchievment("General", "Test", "This is the Description", 15, 0);
        CreateAchievment("General", "Test", "This is the Description", 15, 0);
        CreateAchievment("General", "Test", "This is the Description", 15, 0);
        CreateAchievment("General", "Test", "This is the Description", 15, 0);
        CreateAchievment("General", "Test", "This is the Description", 15, 0);

        CreateAchievment("Other", "Test", "This is the Description", 15, 0);
        CreateAchievment("Other", "Test", "This is the Description", 15, 0);
        CreateAchievment("Other", "Test", "This is the Description", 15, 0);
        CreateAchievment("Other", "Test", "This is the Description", 15, 0);
        CreateAchievment("Other", "Test", "This is the Description", 15, 0);

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
	}

    public void CreateAchievment(string catagory, string title, string description, int points, int spriteIndex)
    {
        GameObject achievment = (GameObject)Instantiate(achievmentPrefab);

        SetAchievmentInfo(catagory, achievment, title, description, points, spriteIndex);
    }

    public void SetAchievmentInfo(string catagory, GameObject achievment, string title, string description, int points, int spriteIndex)
    {
        achievment.transform.SetParent(GameObject.Find(catagory).transform);
        achievment.transform.localScale = new Vector3(1, 1, 1);
        achievment.transform.GetChild(0).GetComponent<Text>().text = title;
        achievment.transform.GetChild(1).GetComponent<Text>().text = description;
        achievment.transform.GetChild(2).GetComponent<Text>().text = points.ToString();
        achievment.transform.GetChild(3).GetComponent<Image>().sprite = sprites[spriteIndex];
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
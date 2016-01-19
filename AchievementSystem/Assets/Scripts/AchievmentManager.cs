using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievmentManager : MonoBehaviour 
{
    //생성될 아키브먼트 페널 게임오브젝트
    public GameObject achievmentPrefab;

    public Sprite[] sprites;

    private AchievmentButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievmentMenu;

    //보여질 게임오브젝트
    public GameObject visualAchievment;

    public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>();

	// Use this for initialization
	void Start () 
    {
        //Example
        //achievments.Add("RunAchievment", new Achievment("Run", "He ran", 10, 0, this.gameObject));

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievmentButton>();

        CreateAchievment("Genaral", "Press W", "Press W to unlock this achievment", 5, 0);

        // TODO 주석을 풀것
        //foreach (GameObject achievment in GameObject.FindGameObjectsWithTag("AchievmentList"))
        //{
        //    achievment.SetActive(false);
        //}

        //activeButton.Click();

        //achievmentMenu.SetActive(false);
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

    //https://youtu.be/eJo0rYlxajk?t=901
    public void EarnAchievment(string title)
    {
        if (achievments[title].EarnAchievment())
        {
            //todo: EarnAchievment 제작해야함
            GameObject achievment = (GameObject)Instantiate(visualAchievment);
            SetAchievmentInfo("EarnCanvas", achievment, title);

            StartCoroutine(HideAchievment(achievment));
        }
    }

    //https://youtu.be/eJo0rYlxajk?t=1038
    public IEnumerator HideAchievment(GameObject achiement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achiement);
    }

    public void CreateAchievment(string category, string title, string description, int points, int spriteIndex)
    {
        //instance achievmentPrefab을 achievment로 생성한다.
        GameObject achievment = (GameObject)Instantiate(achievmentPrefab);

        //Achievment 클레스인 newAchievment 인스턴스를 생성한다.
        //newAchievment를 생성할때 name 아니라 title인것 같아 변경한다.
        //https://youtu.be/eJo0rYlxajk?t=843
        Achievment newAchievment = new Achievment(title, description, points, spriteIndex, achievment);

        print(newAchievment.Name);

        //생각한거와 다르게 tile은 여기서 넘겨 주는 것 같다.
        achievments.Add(title, newAchievment);

        //SetAchievmentInfo(category, achievment, title);
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
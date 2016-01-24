using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Achievment
{
    private string name;
    //name의 캡슐화
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string description;
    //description의 캡슐화
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int point;

    public int Point
    {
        get { return point; }
        set { point = value; }
    }

    private int spriteIndex;

    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }

    private GameObject achievmentRef;

	public Achievment(string name, string description, int point, int spriteIndex, GameObject achievmentRef)
    {
        this.name = name;
        this.description = description;
        this.point = point;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievmentRef = achievmentRef;
        Debug.Log("HI");
    }

    public bool EarnAchievment()
    {
        if (!unlocked)
        {
            //https://youtu.be/272UGSKcMd8?t=381
            PlayerPrefs.SetInt("Points", 10);
            PlayerPrefs.Save();
            int myPoints = PlayerPrefs.GetInt("Points");

            //https://youtu.be/272UGSKcMd8?t=242
            achievmentRef.GetComponent<Image>().sprite = AchievmentManager.Instance.unlockedSprite;
            unlocked = true;
            return true;
        }

        return false;
    }

    //https://youtu.be/272UGSKcMd8?t=495
    public void SaveAchivment(bool value)
    {
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints + point);

        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAchievment()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            AchievmentManager.Instance.pointText.text = "Points: " + PlayerPrefs.GetInt("Points");
        }
    }
}

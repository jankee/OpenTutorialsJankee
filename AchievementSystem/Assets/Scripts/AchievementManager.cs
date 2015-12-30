using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour 
{
    public GameObject achievementPrefab;

    public Sprite[] sprites;

	// Use this for initialization
	void Start () 
    {
        CreateAchievement("General", "Test", "This is the Description", 15, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void CreateAchievement(string catagory, string title, string description, int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        SetAchievementInfo(catagory, achievement, title, description, points, spriteIndex);
    }

    public void SetAchievementInfo(string catagory, GameObject achievement, string title, string description, int points, int spriteIndex)
    {
        achievement.transform.SetParent(GameObject.Find(catagory).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[spriteIndex];
    }
}

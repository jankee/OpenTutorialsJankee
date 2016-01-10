using UnityEngine;
using System.Collections;

public class Achievment
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string description;

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

	public Achievment(string name, string description, int point, int spriteIndex, GameObject achievementRef)
    {
        this.name = name;
        this.description = description;
        this.point = point;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievmentRef = achievmentRef;
    }

    public bool EarnAchievment()
    {
        if (unlocked == false)
        {
            unlocked = true;
            return true;
        }

        return false;
    }

}

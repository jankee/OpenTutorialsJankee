using UnityEngine;
using System.Collections;

public enum ItemType
{
    MANA, 
    HEALTH,
};

public enum Quality
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDATY,
    ARTIFACT,
};

public class Item : MonoBehaviour 
{
    public ItemType type;

    public Quality quality;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int maxSize;

    public float strength, intellect, agility, stamina;

    public string itemName;

    public string description;

	// Use this for initialization
	public void Use() 
    {
        switch (type)
        {
            case ItemType.MANA:
                print("I just used a mana potion");
                break;
            case ItemType.HEALTH:
                print("I just used a health potion");
                break;
        }	
	}

    public string GetTooltip()
    {
        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (description != string.Empty)
        {
            newLine = "\n";
        }

        switch (quality)
        {
            case Quality.COMMON:
                color = "white";
                break;
            case Quality.UNCOMMON:
                color = "lime";
                break;
            case Quality.RARE:
                color = "navy";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDATY:
                color = "orange";
                break;
            case Quality.ARTIFACT:
                color = "red";
                break;
        }

        if (strength > 0)
        {
            stats += "\n" + strength.ToString() + "Strength";
        }
        if (intellect > 0)
        {
            stats += "\n" + intellect.ToString() + "Intellect";
        }
        if (agility > 0)
        {
            stats += "\n" + agility.ToString() + "Agility";
        }
        if (stamina > 0)
        {
            stats += "\n" + stamina.ToString() + "Stamina";
        }
        return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + 
            "{1}</color></i>{2}</size>", itemName, description, stats);
    }
}

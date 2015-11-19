using UnityEngine;
using System.Collections;

public enum ItemType
{
    CONSUMEABLE,
    MAINHAND,
    TWOHAND,
    OFFHAND,
    HEAD,
    NECK,
    CHEST,
    RING,
    LEGS,
    BRACERS,
    BOOTS,
    TRINKET,
}

public enum Quality
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY,
    ARTIFACT,
}


public class ItemScript : MonoBehaviour 
{

    public ItemType itemType;

    public Quality quality;

    public Sprite spriteNormal;
    public Sprite spriteHighlight;

    public int maxSize;

    public float strength, intellect, agility, stamina;

    public string itemName;

    public string description;

    public void Use()
    {
        switch (itemType)
        {
            //case ItemType.MANA:
            //    print("I just used a mana potion");
            //    break;
            //case ItemType.HEALTH:
            //    print("I just used a health potion");
            //    break;
        }
    }

    public string GetToolTip()
    {
        string state = string.Empty;
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
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTIFACT:
                color = "red";
                break;
        }

        if (strength > 0)
        {
            state += "\n" + strength.ToString() + " Strength";
        }
        if (intellect > 0)
        {
            state += "\n" + strength.ToString() + " Intellect";
        }
        if (agility > 0)
        {
            state += "\n" + strength.ToString() + " Agility";
        }
        if (stamina > 0)
        {
            state += "\n" + strength.ToString() + " Stamina";
        }

        return string .Format("<color=" + color + "><size=16><b>{0}</b></size></color><size=14><i><color=lime>"
            + newLine + "{1}</color></i>{2}</size>", itemName, description, state);
    }

    public void SetStats(Item item)
    {
        //this.itemType = item.itemType;

        //this.quality = item.quality;

        //this.spriteNormal = item.spriteNormal;

        //this.spriteHighlight = item.spriteHighlight;

        //this.maxSize = item.maxSize;

        //this.strength = item.strength;

        //this.intellect = item.intellect;

        //this.agility = item.agility;

        //this.stamina = item.stamina;

        //this.itemName = item.itemName;

        //this.description = item.description;

        switch (itemType)
        {
            //case ItemType.MANA:
            //    GetComponent<Renderer>().material.color = Color.blue;
            //    break;
            //case ItemType.HEALTH:
            //    GetComponent<Renderer>().material.color = Color.red;
            //    break;
            //case ItemType.WEAPON:
            //    GetComponent<Renderer>().material.color = Color.green;
            //    break;
        }
    }

}

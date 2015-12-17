using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

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

    private Item item;

    public Item Item
    {
        get {return item;}
        set {item = value;}
    }

    

    public void Use()
    {

    }

    public string GetToolTip()
    {
        return item.GetToolTip();

        //string state = string.Empty;
        //string color = string.Empty;
        //string newLine = string.Empty;

        //if (description != string.Empty)
        //{
        //    newLine = "\n";
        //}

        //switch (quality)
        //{
        //    case Quality.COMMON:
        //        color = "white";
        //        break;
        //    case Quality.UNCOMMON:
        //        color = "lime";
        //        break;
        //    case Quality.RARE:
        //        color = "navy";
        //        break;
        //    case Quality.EPIC:
        //        color = "magenta";
        //        break;
        //    case Quality.LEGENDARY:
        //        color = "orange";
        //        break;
        //    case Quality.ARTIFACT:
        //        color = "red";
        //        break;
        //}

        //if (strength > 0)
        //{
        //    state += "\n" + strength.ToString() + " Strength";
        //}
        //if (intellect > 0)
        //{
        //    state += "\n" + strength.ToString() + " Intellect";
        //}
        //if (agility > 0)
        //{
        //    state += "\n" + strength.ToString() + " Agility";
        //}
        //if (stamina > 0)
        //{
        //    state += "\n" + strength.ToString() + " Stamina";
        //}

        //return string .Format("<color=" + color + "><size=16><b>{0}</b></size></color><size=14><i><color=lime>"
        //    + newLine + "{1}</color></i>{2}</size>", itemName, description, state);
    }

}

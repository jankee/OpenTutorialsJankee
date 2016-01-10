using UnityEngine;
using System.Collections;

public abstract class Item 
{
    public ItemType ItemType
    {
        get;
        set;
    }

    public Quality Quality
    {
        get;
        set;
    }

    public string SpriteNeutral
    {
        get;
        set;
    }

    public string SpriteHighlighted
    {
        get;
        set;
    }

    public int MaxSize
    {
        get;
        set;
    }

    public string ItemName
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public Item()
    {

    }

    public Item(string itemName, string description, ItemType itemType, Quality quality,
        string spriteNeutral, string spriteHighlighted, int maxSize)
    {
        ItemName = itemName;
        Description = description;
        ItemType = itemType;
        Quality = quality;
        SpriteNeutral = spriteNeutral;
        SpriteHighlighted = spriteHighlighted;
        MaxSize = maxSize;
    }

    public virtual string GetTooltip()
    {
        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (Description != string.Empty)
        {
            newLine = "\n";
        }

        switch (Quality)
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

        return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine +
            "{1}</color></i>{2}</size>", ItemName, Description);

        //return null;
    }

    public abstract void Use();
}

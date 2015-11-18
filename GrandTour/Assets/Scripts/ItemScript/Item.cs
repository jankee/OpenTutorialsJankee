using UnityEngine;
using System.Collections;

public abstract class Item 
{
    public ItemType ItemType { get; set; }

    public Quality Quality { get; set; }

    public string SpriteNeutral { get; set; }

    public string SpriteHighlight { get; set; }

    public int MaxSize { get; set; }

    public string ItemName { get; set; }

    public string Description { get; set; }

    public Item()
    {

    }

    public Item(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlight, int maxSize )
    {
        ItemName = itemName;
        Description = description;
        ItemType = itemType;
        Quality = quality;
        SpriteNeutral = spriteNeutral;
        SpriteHighlight = spriteHighlight;
        MaxSize = maxSize;
    }

    public string GetToolTip()
    {
        return null;
    }

    public abstract void Use()
    {

    }
}
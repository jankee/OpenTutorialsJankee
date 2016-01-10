using UnityEngine;
using System.Collections;

public class Equipment : Item
{
    public int Intellect { get; set; }

    public int Agility { get; set; }

    public int Stamina { get; set; }

    public int Strength { get; set; }

    public Equipment()
    {

    }

    public Equipment(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral,
        string spriteHighlight, int maxSize, int intellect, int agility, int stamina, int strength)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlight, maxSize)
    {
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        Strength = strength;
    }

    public override void Use()
    {

    }

    public override string GetToolTip()
    {
        return base.GetToolTip();
    }
}

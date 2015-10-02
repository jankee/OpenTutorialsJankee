using UnityEngine;
using System.Collections;

public class Equipment : Item 
{
    public int Intellect
    { 
        get; 
        set; 
    }
    public int Agility
    {
        get;
        set;
    }
    public int Stamina
    {
        get;
        set;
    }
    public int Strength
    {
        get;
        set;
    }

    public Equipment()
    {

    }

    public Equipment(string itemName, string description, ItemType itemType, Quality quality,
        string spriteNeutral, string spriteHighlighted, int maxSize, int intellect, int agility, int stamina, int strength) :
        base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    {
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.Strength = strength;
    }


    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public override string GetTooltip()
    {
        return base.GetTooltip();
    }
}

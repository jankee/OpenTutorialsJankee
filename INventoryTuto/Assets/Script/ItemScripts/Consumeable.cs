using UnityEngine;
using System.Collections;

public class Consumeable : Item 
{
    public int Health
    {
        get;
        set;
    }

    public int Mana
    {
        get;
        set;
    }

    public Consumeable()
    {

    }

    public Consumeable(string itemName, string description, ItemType itemType, Quality quality,
        string spriteNeutral, string spriteHighlighted, int maxSize, int health, int mana) : 
        base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize)
    {
        this.Health = health;
        this.Mana = mana;
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

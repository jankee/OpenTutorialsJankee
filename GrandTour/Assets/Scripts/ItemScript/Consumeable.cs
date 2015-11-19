using UnityEngine;
using System.Collections;

public class Consumeable : Item
{
    public int Health { get; set; }

    public int Mana { get; set; }

    public Consumeable()
    { }

    public Consumeable(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral,
        string spriteHighlight, int maxSize, int health, int mana) 
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlight, maxSize)
    {
        Health = health;
        Mana = mana;
    }

    public override void Use()
    {

    }

    public override string GetToolTip()
    {
        return base.GetToolTip();
    }
}

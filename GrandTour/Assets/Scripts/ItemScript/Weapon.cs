using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    public float AttackSpeed { get; set; }


	public Weapon()
    {

    }

    public Weapon(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral,
        string spriteHighlight, int maxSize, int intellect, int agility, int stamina, int strength, float attackSpeed)
        : base(itemName, description, itemType, quality, spriteNeutral, spriteHighlight, maxSize, intellect, agility, stamina, strength)
    {
        AttackSpeed = attackSpeed;
    }

    public override void Use()
    {

    }

    public override string GetToolTip()
    {
        return base.GetToolTip();
    }
}

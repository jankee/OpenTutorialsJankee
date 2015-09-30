using UnityEngine;
using System.Collections;

public class Weapon : Equipment 
{

    public float AttackSpeed
    {
        get;
        set;
    }


    public Weapon()
    {

    }

    public Weapon(string itemName, string description, ItemType itemType, Quality quality,
        string spriteNeutral, string spriteHighlighted, int maxSize, int intelliect, int agility, int stamina, int strength, float attackSpeed) :
        base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, intelliect, agility, stamina, strength)
    {
        this.AttackSpeed = attackSpeed;
    }

    public override string GetTooltip()
    {
        return base.GetTooltip();
    }

}

﻿using UnityEngine;
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
        string spriteNeutral, string spriteHighlighted, int maxSize, int intellect, int agility, int stamina, int strength, float attackSpeed) :
        base(itemName, description, itemType, quality, spriteNeutral, spriteHighlighted, maxSize, intellect, agility, stamina, strength)
    {
        this.AttackSpeed = attackSpeed;
    }

    public override string GetTooltip()
    {
        string equipmentTip = base.GetTooltip();

        string stats = string.Empty;

        //if (AttackSpeed > 0)
        //{
        //    stats += "\n Restores" + AttackSpeed.ToString() + " Attack Speed";
        //}

        return string.Format("{0}" + "<size=14> {1} </size>", equipmentTip, AttackSpeed);
    }

}
using UnityEngine;
using System.Collections;

public enum Catagory
{
    EQUIPMENT,
    WEAPON,
    CONSUMEABLE,
}

public class ItemManager : MonoBehaviour 
{
    public ItemType itemType;
    public Quality quality;
    public Catagory catagory;
    public string spriteNeutral;
    public string spriteHighlighted;
    public string itemName;
    public string description;
    public int maxSize;
    public int intellect;
    public int agility;
    public int stamina;
    public int strength;
    public float attackSpeed;
    public int health;
    public int mana;
}

using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

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

    public void CreateItem()
    {
        ItemContainer itemContainer = new ItemContainer();

        Type[] itemTypes = { typeof(Weapon), typeof(Equipment), typeof(Consumeable) };

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        serializer.Serialize(fs, itemContainer);

        fs.Close();

        switch (catagory)
        {
            case Catagory.EQUIPMENT:
                itemContainer.Equipment.Add(new Equipment(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlighted, maxSize, intellect, agility, stamina, strength));
                break;
            case Catagory.WEAPON:
                itemContainer.Weapons.Add(new Weapon(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlighted, maxSize, intellect, agility, stamina, strength, attackSpeed));
                break;
            case Catagory.CONSUMEABLE:
                itemContainer.Consumeables.Add(new Consumeable(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlighted, maxSize, health, mana));
                break;
        }

        fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Create);
        serializer.Serialize(fs, itemContainer);
        fs.Close();
    }
}

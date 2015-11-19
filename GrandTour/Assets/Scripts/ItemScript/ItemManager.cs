using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

public enum Catagory 
{
    EQUIPMENT,
    CONSUMEABLE,
    WEAPON,
}

public class ItemManager : MonoBehaviour {

    public ItemType itemType;

    public Quality quality;

    public Catagory catagoty;

    public string spriteNeutral;

    public string spriteHighlight;

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

        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable) };

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        serializer.Serialize(fs, itemContainer);

        fs.Close();

        switch (catagoty)
        {
            case Catagory.EQUIPMENT:
                itemContainer.Eqipment.Add(new Equipment(itemName, description, itemType, quality, spriteNeutral, spriteHighlight,
                    maxSize, intellect, agility, stamina, strength));
                break;
            case Catagory.CONSUMEABLE:
                itemContainer.Consumeables.Add(new Consumeable(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlight, maxSize, health, mana));
                break;
            case Catagory.WEAPON:
                itemContainer.Weapons.Add(new Weapon(itemName, description, itemType, quality, spriteNeutral, spriteHighlight,
                    maxSize, intellect, agility, stamina, strength, attackSpeed));
                print(itemContainer.Weapons.Count);
                print(itemContainer.Weapons[0].ItemName);
                break;
        }

        fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Create);
        //StreamWriter ffs = new StreamWriter(Application.streamingAssetsPath, fs("Windows-1252"));
        serializer.Serialize(fs, itemContainer);
        print(fs);
        fs.Close();
    }
}

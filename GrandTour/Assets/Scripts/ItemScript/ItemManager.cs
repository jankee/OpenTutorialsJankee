using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public enum Catagory
{
    EQUIPMENT,
    CONSUMEABLE,
    WEAPON,
}

public class ItemManager : MonoBehaviour
{

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

    string path;


    public void CreateItem()
    {
        path = Application.streamingAssetsPath + "/Items.xml";

        string result = string.Empty;

        //ItemContainer itemContainer = new ItemContainer();

        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable) };

        FileStream stream = new FileStream(path, FileMode.Open);

        StreamReader streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        ItemContainer itemContainer = (ItemContainer)serializer.Deserialize(streamReader);

        stream.Close();

        switch (catagoty)
        {
            case Catagory.EQUIPMENT:
                itemContainer.Eqipment.Add(new Equipment(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlight, maxSize, intellect, agility, stamina, strength));
                print("Equipment");
                break;
            case Catagory.CONSUMEABLE:
                itemContainer.Consumeables.Add(new Consumeable(itemName, description, itemType, quality, spriteNeutral,
                    spriteHighlight, maxSize, health, mana));
                print("Consumeable");
                break;
            case Catagory.WEAPON:
                itemContainer.Weapons.Add(new Weapon(itemName, description, itemType, quality, spriteNeutral, spriteHighlight,
                    maxSize, intellect, agility, stamina, strength, attackSpeed));

                print("Weapon" + itemName + description);

                break;
        }

        stream = new FileStream(path, FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

        serializer.Serialize(streamWriter, itemContainer);

        stream.Close();
    }
}

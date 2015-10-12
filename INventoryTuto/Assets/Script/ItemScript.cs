using UnityEngine;
using System.Collections;

public enum ItemType
{
    CONSUMEABLE, 
    MAINHAND,
    TWOHAND,
    OFFHAND,
    HEAD,
    NECK,
    CHEST,
    RING,
    LEGS,
    BRACERS,
    BOOTS,
    TRINKET,
};

public enum Quality
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDATY,
    ARTIFACT,
};

public class ItemScript : MonoBehaviour 
{
    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    private Item item;
    public Item Item
    {
        get { return item; }
        set 
        { 
            item = value;

            spriteNeutral = Resources.Load<Sprite>(value.SpriteNeutral);
            spriteHighlighted = Resources.Load<Sprite>(value.SpriteHighlighted);
        }
    }

	// Use this for initialization
	public void Use() 
    {

	}

    public string GetTooltip()
    {
        return item.GetTooltip();

        //if (strength > 0)
        //{
        //    stats += "\n" + strength.ToString() + " Strength";
        //}
        //if (intellect > 0)
        //{
        //    stats += "\n" + intellect.ToString() + " Intellect";
        //}
        //if (agility > 0)
        //{
        //    stats += "\n" + agility.ToString() + " Agility";
        //}
        //if (stamina > 0)
        //{
        //    stats += "\n" + stamina.ToString() + " Stamina";
        //}
        //return string.Format("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=lime>" + newLine + 
        //    "{1}</color></i>{2}</size>", itemName, description, stats);
    }

}

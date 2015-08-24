using UnityEngine;
using System.Collections;

public enum ItemType
{
    MANA, 
    HEALTH,
};

public class Item : MonoBehaviour 
{
    public ItemType type;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int maxSize;



	// Use this for initialization
	public void Use() 
    {
        switch (type)
        {
            case ItemType.MANA:
                print("I just used a mana potion");
                break;
            case ItemType.HEALTH:
                print("I just used a health potion");
                break;
        }	
	}
}

using UnityEngine;
using System.Collections;

public enum ItemType
{
    MANA,
    HEALTH,
}


public class Item : MonoBehaviour {

    public ItemType itemType;

    public Sprite spriteNormal;
    public Sprite spriteHighlight;

    public int maxSize;
    
    public void Use(ItemType itemType)
    {
        switch (itemType)
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

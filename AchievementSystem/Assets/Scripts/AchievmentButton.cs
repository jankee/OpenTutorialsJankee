using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievmentButton : MonoBehaviour 
{
    public GameObject achievmentList;

    public Sprite neutral, highlight;

    private Image sprite;

    

    public void Awake()
    {
        sprite = GetComponent<Image>();
    }
    
    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            achievmentList.SetActive(true);
        }
        else
        {
            sprite.sprite = neutral;
            achievmentList.SetActive(false);
        }
    }
}

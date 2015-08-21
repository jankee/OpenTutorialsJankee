﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{

    private Stack<Item> items;

    public Text stackTxt;

    public Sprite slotEmpty;
    public Sprite slotHighlight;

    //스탁의 카운트가 0이면 참을 준다
    public bool isEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public bool isAvarable
    {
        get
        {
            return currentItem.maxSize > items.Count;
        }
    }

    public Item currentItem
    {
        get
        {
            return items.Peek();
        }
    }

	// Use this for initialization
	void Start () 
    {
        //아이템스를 초기화 한다
        items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = GetComponent<RectTransform>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.6);

        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        //txtRect의 가로세로 사이즈 구한다.
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        
	}

    public void AddItem(Item item)
    {
        //리스트 아이템스에 아이템을 넣어준다.
        items.Push(item);
        //아이템스의 갯수가 1이상되면 stackTxt에 카운트만큼 표시를 해준다.
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        ChaingeSprite(item.spriteNeutral, item.spriteHighlighted);
    }


    private void ChaingeSprite(Sprite neutral, Sprite highlight)
    {
        this.GetComponent<Image>().sprite = neutral;

        //스프라이트 스테이트 st를 초기화 한다.
        SpriteState st = new SpriteState();

        //각 스프라이트 상황에 맞게 스프라이트를 넣어준다.
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        //버튼 컨포넌트 스프라이트스테이트에 st를 대응한다.
        this.GetComponent<Button>().spriteState = st;
    }
}

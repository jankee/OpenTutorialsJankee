using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{

    private Stack<Item> items;

    public Text stackTxt;

    public Sprite slotNormal;

    public Sprite slotHighlight;

	// Use this for initialization
	void Start () 
    {
        items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = GetComponent<RectTransform>();

        //text의 리사이즈텍스트에 사용 할 변수를 만든다.
        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.6f);
        stackTxt.resizeTextMinSize = txtScaleFactor;
        stackTxt.resizeTextMaxSize = txtScaleFactor;


        //왜 이러는지 모르겠음
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void AddItem(Item item)
    {
        //item을 받아서 items안에 넣는다.
        items.Push(item);

        //items의 갯수가 1이상일때 stackTxt에 items의 갯수를 써 준다.
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        print(items.Count);
    }

    public void ChaingeSprite(Sprite normal, Sprite highlight)
    {
        //이미지 컴포넌트 스프라이트에 노말이미지를 넣어 준다.
        GetComponent<Image>().sprite = normal;

        //SpriteState의 st변수를 만든다.
        SpriteState st = new SpriteState();

        //st 하이라이트에 하이라이트를 넣어준다.
        st.highlightedSprite = highlight;
        //st 눌렀을때 노말을 넣어준다.
        st.pressedSprite = normal;

        //버튼 컴퍼넌트의 spriteState에 st를 넣어준다.
        GetComponent<Button>().spriteState = st;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    private Stack<Item> items;

    public Stack<Item> Items
    {
        get { return items; }
        set { items = value; }
    }

    public Text stackTxt;

    public Sprite slotNormal;

    public Sprite slotHighlight;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    //currentItem의 maxSize 스탁 items의 카운트 갯수와 비교하여 maxSize값이 크면 참이다.
    public bool IsAvailable
    {
        get
        {
            return currentItem.maxSize > items.Count;
        }
    }


    //스탁 items에서 하나를 선택하여 currentItem에 넣어 준다
    public Item currentItem
    {
        get
        {
            return items.Peek();
        }
    }

    // Use this for initialization
    void Start()
    {
        items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        //text의 리사이즈텍스트에 사용 할 변수를 만든다.
        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.6f);
        stackTxt.resizeTextMinSize = txtScaleFactor;
        stackTxt.resizeTextMaxSize = txtScaleFactor;


        //왜 이러는지 모르겠음
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //스롯에 스탁 items에 item을 저장한다.  
    public void AddItem(Item item)
    {
        //item을 받아서 items안에 넣는다.
        items.Push(item);

        //items의 갯수가 1이상일때 stackTxt에 items의 갯수를 써 준다.
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        ChangeSprite(item.spriteNormal, item.spriteHighlight);

        //아이템의 타잎과 사용을 해보았다.
        //item.Use(item.itemType);
    }

    public void AddItems(Stack<Item> items)
    {
        //기존 items에 인자로 받은 items로 초기화 해준다.
        this.items = new Stack<Item>(items);

        //stackTxt의 Text에 item 갯수를 표시한다.
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        //스프라이트 이미지를 currentItem의 이미지로 교체한다.
        ChangeSprite(currentItem.spriteNormal, currentItem.spriteHighlight);
    }

    public void ClearSlot()
    {
        items.Clear();

        stackTxt.text = string.Empty;

        ChangeSprite(slotNormal, slotHighlight);

    }

    private void ChangeSprite(Sprite normal, Sprite highlight)
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

    private void UseItem()
    {
        if (!IsEmpty)
        {
            items.Pop().Use();

            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        }
        if (IsEmpty)
        {
            print("Empty");
            ChangeSprite(slotNormal, slotHighlight);
            Inventory.EmptySlot++;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //오른쪽 클릭을 했을 때 Hover를 못찾았을 때 캔버스그릅의 알파가 0 이상일때
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover")
            && Inventory.CanvasGroup.alpha > 0)
        {
            //slot에 item을 사용한다
            UseItem();
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{

    private RectTransform inventoryRect;

    private float inventoryHight, inventoryWidth;

    public int slots;

    public int rows;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize;

    public GameObject slotPrefab;

    private List<GameObject> allSlots;

    //비여있는 스롯 갯수 변수
    private static int emptySlot;

    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

	// Use this for initialization
	void Start () 
    {
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slot slot = GameObject.FindObjectOfType<Slot>();

            Item tt = GameObject.Find("Mana").GetComponent<Item>();



            AddItem(tt);

        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Slot tmp = 
        //}
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        //비여있는 스롯갯수에 생성된 스롯갯수를 넣는다.
        emptySlot = slots;

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;

        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        inventoryRect = GetComponent<RectTransform>();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

        int colums = slots / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colums; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),
                    -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }
        }
    }


    public bool AddItem(Item item)
    {
        //item의 maxSize가 1일때 빈 스롯에 찾아서 넣어 준다
        if (item.maxSize == 1)
        {
            //빈 슬롯 함수에 아이템을 넘겨 준다.
            PlaceEmpty(item);
            return true;
        }
        else
        {
            //모든 슬롯중에 아이템이 들어 있는 슬롯을 찾아 현채 아이템 같으면 추가를 해준다.
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();
                //비여있지 않은 슬롯이면
                if (!tmp.IsEmpty)
                {
                    //아이템 타입이 같고 maxSize가 여유가 있다면
                    if (tmp.currentItem.itemType == item.itemType && tmp.IsAvailable)
                    {
                        //슬롯의 AddItem으로 아이템을 넣어 준다.
                        tmp.AddItem(item);

                        return true;
                    }
                }
            }

            if (emptySlot > 0)
            {
                PlaceEmpty(item);

            }
        }
        return false;
    }


    private bool PlaceEmpty(Item item)
    {
        if (emptySlot > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.IsEmpty)
                {
                    tmp.AddItem(item);
                    emptySlot--;
                    return true;
                }
            }
        }

        return false;
    }
}

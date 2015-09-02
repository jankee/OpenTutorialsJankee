using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour 
{
    //인벤의 크기를 조정할 변수
    private RectTransform inventoryRect;
    //인벤의 가로와 세로 값 변수
    private float inventoryWidth, inventoryHight;

    //슬롯의 갯수 변수
    public int slot;
    //슬롯의 열의 갯수 변수
    public int rows;

    //슬롯의 왼쪽 간격, 윗쪽 간격 변수
    public float slotPaddingLeft, slotPaddingTop;

    //슬롯의 크기 변수
    public float slotSize;

    //슬롯 오브젝트 변수
    public GameObject slotPrefab;

    private static Slot from, to;

    //슬롯 오브젝트 리스트 변수
    private List<GameObject> allSlots;

    //아이콘 이미지를 등록한다.
    public GameObject iconPrefab;

    //등록된 아이콘이미지를 관리할 스테틱 변수
    private static GameObject hoverObject;

    public Camera camera;
    public Canvas canvas;

    public EventSystem eventSystem;

    //비여있는 스롯 갯수 저장 변수
    private static int emptySlot;

    private float hoverYOffset;

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
        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));

                to = null;
                from = null;
                hoverObject = null;
            }
        }

        if (hoverObject != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, camera, out position);

            position.Set(position.x, position.y - hoverYOffset);

            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }
	
	}

    private void CreateLayout()
    {
        //allSlot의 초기화
        allSlots = new List<GameObject>();

        hoverYOffset = slotSize * -10f;

        //비여있는 스롯 갯수를 초기화
        emptySlot = slot;

        int colums = slot / rows;

        //스롯 싸이즈와 페딩 싸이즈로 인벤토리의 크기를 결정한다.
        inventoryWidth = colums * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        //인벤을 RectTransform으로 초기화 한다.
        inventoryRect = GetComponent<RectTransform>();
        //초기화 된 인벤에 가로 세로값을 적용
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colums; x++)
            {

                //스롯의 x, y 포지션 값을 구하는 변수
                float xPosition = slotPaddingLeft * (x + 1) + (slotSize * x);
                float yPosition = -slotPaddingTop * (y + 1) + -(slotSize * y);

                //newSlot에 slotPrefab 오브젝트를 넣어준다.
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);
                //slotRect에 newSlot의 렉트컨포넌트를 넣준다.
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                //newSlot 이름을 Slot이라 정해준다
                newSlot.name = "Slot";
                //newSlot의 부모를 cavas롤 변경해준다
                newSlot.transform.SetParent(this.transform.parent);
                //slotRect의 포지션을 정해준다
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(xPosition, yPosition, 0);
                //사이즈 값을 slotSize로 넣어 준다
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                //스케일값 변경때문에 다시 설정
                //slotRect.localScale = new Vector3(1, 1, 1);
                //allSlot에 동록한다
                allSlots.Add(newSlot);
            }
        }
    }

    public bool AddItem(Item item)
    {
        if (item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        else
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (!tmp.isEmpty)
                {
                    if (tmp.currentItem.type == item.type && tmp.isAvailable)
                    {
                        tmp.AddItem(item);
                        //emptySlot--;
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

    //비여있는 슬롯을 찾아 아이템을 넣어 준다.
    private bool PlaceEmpty(Item item)
    {
        if (emptySlot > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.isEmpty)
                {
                    tmp.AddItem(item);
                    emptySlot--;
                    return true;

                }
            }
        }
        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        //from 슬롯이 비여 있다면
        if (from == null)
        {
            //클릭한 슬롯이 비여있지 않다면
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                //from에 클릭 스롯을 넣어준다
                from = clicked.GetComponent<Slot>();
                //from의 이미지 컨포넌트에 컬러르 회색으로 바꾸어 준다
                from.GetComponent<Image>().color = Color.gray;

                //하버이미지에 아이콘을 넣어준다.
                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

                hoverObject.transform.localScale = from.gameObject.transform.localScale;

            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slot>();

            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && from != null)
        {
            Stack<Item> tmpTo = new Stack<Item>(to.Items);

            to.AddItems(from.Items);

            if (tmpTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }

            from.GetComponent<Image>().color = Color.white;

            to = null;
            from = null;
            hoverObject = null;
        }

    }
}

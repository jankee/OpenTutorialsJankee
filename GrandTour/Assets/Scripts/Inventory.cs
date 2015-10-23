using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{

    private RectTransform inventoryRect;

    private float inventoryHight, inventoryWidth;

    public int slots;

    public int rows;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize;

    public GameObject slotPrefab;

    public GameObject iconPrefab;

    private static GameObject hoverObj;

    private static Slot from, to;

    private List<GameObject> allSlots;

    //비여있는 스롯 갯수 변수
    private static int emptySlot;

    public Canvas canvas;

    private float hoverOffset;

    public EventSystem eventSystem;

    private static CanvasGroup canvasGroup;

    public static CanvasGroup CanvasGroup
    {
        get { return Inventory.canvasGroup; }
    }

    private bool fadingOut, fadingIn;

    public float fadingTime;

    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

    // Use this for initialization
    void Start()
    {
        canvasGroup = gameObject.transform.parent.GetComponent<CanvasGroup>();

        CreateLayout();
    }

    // Update is called once per frame
    void Update()
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
                hoverObj = null;
                emptySlot++;
            }
        }

        if (hoverObj != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera, out position);

            position.Set(position.x + hoverOffset, position.y - hoverOffset);

            hoverObj.transform.position = canvas.transform.TransformPoint(position);
            //내가 생각한 offset방법
            //hoverObj.transform.position += new Vector3(5, -5, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Slot slot = GameObject.FindObjectOfType<Slot>();

            //Slot from = GameObject.Find("Mana").GetComponent<Slot>();

            foreach (GameObject item in allSlots)
            {
                Slot tmp = item.GetComponent<Slot>();

                if (!tmp.IsEmpty)
                {
                    MoveItem(tmp.GetComponent<GameObject>());

                    
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (canvasGroup.alpha > 0)
            {
                print("Hi");
                StartCoroutine("FadeOut");
            }
            else
            {
                StartCoroutine("FadeIn");
            }
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Slot tmp = 
        //}
    }

    private void CreateLayout()
    {

        allSlots = new List<GameObject>();

        hoverOffset = slotSize * 0.1f;

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

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);

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

    public void MoveItem(GameObject clicked)
    {

        Slot tmp = clicked.GetComponent<Slot>();

        //print(clicked.transform.name);

        //from이 비여있을때
        if (from == null)
        {
            //인자 clicked가 비여있지 않다면
            if (!clicked.GetComponent<Slot>().IsEmpty)
            {
                //from에 clicked 인자를 넣어 준다
                from = clicked.GetComponent<Slot>();
                //from의 Image컨퍼넌트를 이용하여 컬러를 회색으로 바꾸어 준다
                from.GetComponent<Image>().color = Color.grey;

                hoverObj = (GameObject)Instantiate(iconPrefab);
                hoverObj.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObj.gameObject.name = "Hover";

                RectTransform hoverTransform = hoverObj.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObj.transform.SetParent(GameObject.Find("Canvas").transform, true);
            }
        }
        //to가 비여있다면
        else if (to == null)
        {
            //to에 인자로 받은 clicked를 넣어 준다
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }
        //to, from 둘 다 비여있다면
        if (from != null && to != null)
        {
            //tmpTo에 to를 넣어 준다
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            //to에 from을 넣어 준다
            to.AddItems(from.Items);

            //tmpTo의 갯수가 0 일때
            if (tmpTo.Count == 0)
            {
                //from에 slot을 지워 준다
                from.ClearSlot();
            }
            else
            {
                //tmpTo가 0이 아닐때 from에 tmpTo를 넣어준다
                from.AddItems(tmpTo);
            }

            //from에 이미지 컴포넌트를 이용하여 컬러를 화이트로 바꿔준다
            from.GetComponent<Image>().color = Color.white;
            //to, from을 비워준다
            to = null;
            from = null;
            hoverObj = null;
            //나름 설정을 해본 것
            //Destroy(hoverObj);
        }
    }

    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;

            float rate = 1.0f / fadingTime;
            float startAlpha = canvasGroup.alpha;
            float progress = 0f;

            StopCoroutine("FadeIn");

            while (progress < 1)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = 0f;

            fadingOut = false;
        }
    }

    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;

            float rate = 1.0f / fadingTime;
            float startAlpha = canvasGroup.alpha;
            float progress = 0f;

            StopCoroutine("FadeOut");

            while (progress < 1)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = 1f;

            fadingIn = false;
        }
    }
}

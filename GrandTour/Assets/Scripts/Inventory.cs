﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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

    private static GameObject clicked;

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

    private static Inventory instance;

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Inventory>();
            }
            return instance;
        }
    }

    private bool fadingOut, fadingIn;

    public float fadingTime;

    public GameObject selectStackSize;

    private static GameObject selectStackSizeStatic;

    public Text stackText;

    private int splitAmount;
    private int maxStackCount;

    private static Slot movingSlot;

    public GameObject mana, health;

    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

    public GameObject toolTipObj;
    private static GameObject toolTip;

    public Text sizeTxtObj;
    private static Text sizeTxt;

    public Text visualTxtObj;
    private static Text visualTxt;

    public GameObject dropItem;

    private static GameObject playerRef;

    // Use this for initialization
    void Start()
    {

        toolTip = toolTipObj;

        sizeTxt = sizeTxtObj;

        visualTxt = visualTxtObj;

        selectStackSizeStatic = selectStackSize;

        playerRef = GameObject.Find("Player");

        canvasGroup = gameObject.transform.parent.GetComponent<CanvasGroup>();

        CreateLayout();

        movingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        //인벤토리 밖에서 클릭을 했을 때
        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                foreach (Item item in from.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    print(angle);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    print(v);

                    //v *= 30;

                    //print(v);

                    GameObject tmpDrop = (GameObject)GameObject.Instantiate(dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpDrop.GetComponent<Item>().SetStats(item);
                }                
                
                from.GetComponent<Image>().color = Color.white;
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));

                to = null;
                from = null;
                emptySlot++;
            }
            else if (!eventSystem.IsPointerOverGameObject(-1) && !movingSlot.IsEmpty)
            {
                foreach (Item item in movingSlot.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    print(angle);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    print(v);

                    //v *= 30;

                    //print(v);

                    GameObject tmpDrop = (GameObject)GameObject.Instantiate(dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpDrop.GetComponent<Item>().SetStats(item);
                }

                movingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
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
                    MoveItem(tmp.gameObject);


                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (canvasGroup.alpha > 0)
            {
                StartCoroutine("FadeOut");
                PutItemBack();
            }
            else
            {
                StartCoroutine("FadeIn");
            }
        }

        if (Input.GetMouseButton(2))
        {
            if (eventSystem.IsPointerOverGameObject(-1))
            {
                MoveInventory();
            }
        }
    }

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        

        if (!tmpSlot.IsEmpty && hoverObj == null == !selectStackSizeStatic.activeSelf)
        {
            visualTxt.text = tmpSlot.currentItem.GetToolTip();
            sizeTxt.text = visualTxt.text;

            toolTip.SetActive(true);

            float xPos = slot.transform.position.x;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y;

            toolTip.transform.position = new Vector2(xPos, yPos);
        }

        
    }

    public void HideToolTip(GameObject slot)
    {
        toolTip.SetActive(false);
    }

    public void SaveInventory()
    {
        string content = "";

        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();

            if (!tmp.IsEmpty)
            {
                content += i + "-" + tmp.currentItem.itemType.ToString() + "-" + tmp.Items.Count.ToString() + ";";
            }
        }

        PlayerPrefs.SetString("content", content);
        PlayerPrefs.SetInt("slots", slots);
        PlayerPrefs.SetInt("rows", rows);
        PlayerPrefs.SetFloat("slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat("slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat("slotSize", slotSize);
        PlayerPrefs.SetFloat("xPos", inventoryRect.position.x);
        PlayerPrefs.SetFloat("yPos", inventoryRect.position.y);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        string content = PlayerPrefs.GetString("content");

        slots = PlayerPrefs.GetInt("slots");
        rows = PlayerPrefs.GetInt("rows");
        slotPaddingLeft = PlayerPrefs.GetFloat("slotPaddingLeft");
        slotPaddingTop = PlayerPrefs.GetFloat("slotPaddingTop");
        slotSize = PlayerPrefs.GetFloat("slotSize");
        inventoryRect.position = new Vector2( PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));

        CreateLayout();

        string[] splitContant = content.Split(';');

        for (int x = 0; x < splitContant.Length - 1; x++)
        {
            string[] splitValues = splitContant[x].Split('-');

            int index = Int32.Parse(splitValues[0]);

            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), splitValues[1]);

            int amount = Int32.Parse(splitValues[2]);

            for (int i = 0; i < amount; i++)
            {
                switch (type)
                {
                    case ItemType.MANA:
                        allSlots[index].GetComponent<Slot>().AddItem(mana.GetComponent<Item>());
                        break;
                    case ItemType.HEALTH:
                        allSlots[index].GetComponent<Slot>().AddItem(health.GetComponent<Item>());
                        break;
                }
            }
        }
    }

    private void CreateLayout()
    {
        if (allSlots != null)
        {
            foreach (GameObject go in allSlots)
            {
                Destroy(go);
            }
        }
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

                newSlot.transform.SetParent(this.transform.FindChild("SlotParent"));

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),
                    -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);

                allSlots.Add(newSlot);
            }
        }
    }

    public void SetStackInfo(int maxStackCount)
    {
        selectStackSizeStatic.SetActive(true);
        toolTip.SetActive(false);
        splitAmount = 0;
        this.maxStackCount = maxStackCount;
        stackText.text = splitAmount.ToString();
    }

    public void SplitStack()
    {
        selectStackSizeStatic.SetActive(false);

        if (splitAmount == maxStackCount)
        {
            MoveItem(clicked);
        }
        else if (splitAmount > 0)
        {
            movingSlot.Items = clicked.GetComponent<Slot>().RemoveItems(splitAmount);

            CreateHover();
        }
    }

    public void ChangeStackText(int i)
    {
        splitAmount += i;

        if (splitAmount < 0)
        {
            splitAmount = 0;
        }
        if (splitAmount > maxStackCount)
        {
            splitAmount = maxStackCount;
        }

        stackText.text = splitAmount.ToString();
    }

    public void MergeStacks(Slot source, Slot destination)
    {
        int max = destination.currentItem.maxSize - destination.Items.Count;

        int count = source.Items.Count < max ? source.Items.Count : max;

        for (int i = 0; i < count; i++)
        {
            destination.AddItem(source.RemoveItem());
            hoverObj.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString()
            : string.Empty;
        }

        if (source.Items.Count == 0)
        {
            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
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
                        if (!movingSlot.IsEmpty && clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else
                        {
                            tmp.AddItem(item);
                            return true;
                        }
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

    public void PutItemBack()
    {
        if (from != null)
        {
            Destroy(GameObject.Find("Hover"));
            from.GetComponent<Image>().color = Color.white;
            from = null;
        }
        else if (!movingSlot.IsEmpty)
        {
            Destroy(GameObject.Find("Hover"));

            foreach (Item item in movingSlot.Items)
            {
                clicked.GetComponent<Slot>().AddItem(item);
            }

            movingSlot.ClearSlot();
        }

        selectStackSize.SetActive(false);
    }

    public void MoveItem(GameObject clicked)
    {

        Inventory.clicked = clicked;

        if (!movingSlot.IsEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                tmp.AddItems(movingSlot.Items);
                movingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.IsEmpty && movingSlot.currentItem.itemType == tmp.currentItem.itemType
                && tmp.IsAvailable)
            {
                MergeStacks(movingSlot, tmp);
            }

        }
        else if (from == null && canvasGroup.alpha == 1 && !Input.GetKey(KeyCode.LeftShift))
        {
            if (!clicked.GetComponent<Slot>().IsEmpty && !GameObject.Find("Hover"))
            {
                from = clicked.GetComponent<Slot>();

                from.GetComponent<Image>().color = Color.gray;

                CreateHover();
            }
        }
        else if (to == null && !Input.GetKey(KeyCode.LeftShift))
        {
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
            Destroy(GameObject.Find("Hover"));
            //나름 설정을 해본 것
            //Destroy(hoverObj);
        }
    }

    public void MoveInventory()
    {
        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
            new Vector2(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * canvas.scaleFactor), Input.mousePosition.y +
            (inventoryRect.sizeDelta.y / 2 * canvas.scaleFactor)), canvas.worldCamera, out mousePos);

        transform.position = canvas.transform.TransformPoint(mousePos);
    }

    public void CreateHover()
    {


        hoverObj = (GameObject)Instantiate(iconPrefab);
        hoverObj.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
        hoverObj.gameObject.name = "Hover";

        RectTransform hoverTransform = hoverObj.GetComponent<RectTransform>();
        RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

        hoverObj.transform.SetParent(GameObject.Find("Canvas").transform, true);

        hoverObj.transform.localScale = clicked.gameObject.transform.localScale;

        hoverObj.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString()
            : string.Empty;
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

using UnityEngine;
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

    private List<GameObject> allSlots;

    //비여있는 스롯 갯수 변수
    private static int emptySlot;

    private float hoverOffset;

    private CanvasGroup canvasGroup;

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



    //private static GameObject selectStackSizeStatic;

    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

    private static GameObject playerRef;

    // Use this for initialization
    void Start()
    {

        //toolTip = toolTipObj;

        //sizeTxt = sizeTxtObj;

        //visualTxt = visualTxtObj;

        //selectStackSizeStatic = InventoryManager.Instance.selectStackSize;

        playerRef = GameObject.Find("Player");

        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        CreateLayout();

        InventoryManager.Instance.MovingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        //인벤토리 밖에서 클릭을 했을 때
        if (Input.GetMouseButtonUp(0))
        {
            if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && InventoryManager.Instance.From != null)
            {
                foreach (Item item in InventoryManager.Instance.From.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    print(angle);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    print(v);

                    //v *= 30;

                    //print(v);

                    GameObject tmpDrop = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpDrop.GetComponent<Item>().SetStats(item);
                }

                InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
                InventoryManager.Instance.From.ClearSlot();
                Destroy(GameObject.Find("Hover"));

                InventoryManager.Instance.To = null;
                InventoryManager.Instance.From = null;
                emptySlot++;
            }
            else if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && !InventoryManager.Instance.MovingSlot.IsEmpty)
            {
                foreach (Item item in InventoryManager.Instance.MovingSlot.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    print(angle);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    print(v);

                    //v *= 30;

                    //print(v);

                    GameObject tmpDrop = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpDrop.GetComponent<Item>().SetStats(item);
                }

                InventoryManager.Instance.MovingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
            }
        }

        if (InventoryManager.Instance.HoverObj != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform,
                Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);

            position.Set(position.x + hoverOffset, position.y - hoverOffset);

            InventoryManager.Instance.HoverObj.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
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

        if (Input.GetMouseButton(2))
        {
            if (InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1))
            {
                MoveInventory();
            }
        }
    }

    public void Open()
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

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();



        if (!tmpSlot.IsEmpty && InventoryManager.Instance.HoverObj == null == !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            InventoryManager.Instance.visualTxtObj.text = tmpSlot.currentItem.GetToolTip();
            InventoryManager.Instance.sizeTxtObj.text = InventoryManager.Instance.visualTxtObj.text;

            InventoryManager.Instance.toolTipObj.SetActive(true);

            float xPos = slot.transform.position.x;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y;

            InventoryManager.Instance.toolTipObj.transform.position = new Vector2(xPos, yPos);
        }


    }

    public void HideToolTip(GameObject slot)
    {
        InventoryManager.Instance.toolTipObj.SetActive(false);
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
        inventoryRect.position = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));

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
                        allSlots[index].GetComponent<Slot>().AddItem(InventoryManager.Instance.mana.GetComponent<Item>());
                        break;
                    case ItemType.HEALTH:
                        allSlots[index].GetComponent<Slot>().AddItem(InventoryManager.Instance.health.GetComponent<Item>());
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
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform);

                slotRect.localPosition = new Vector2(slotPaddingLeft * (x + 1) + (slotSize * x),
                    -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

                allSlots.Add(newSlot);
            }
        }
    }



    public void SplitStack()
    {
        InventoryManager.Instance.selectStackSize.SetActive(false);

        if (InventoryManager.Instance.SplitAmount == InventoryManager.Instance.MaxStackCount)
        {
            MoveItem(InventoryManager.Instance.Clicked);
        }
        else if (InventoryManager.Instance.SplitAmount > 0)
        {
            InventoryManager.Instance.MovingSlot.Items =
                InventoryManager.Instance.Clicked.GetComponent<Slot>().RemoveItems(InventoryManager.Instance.SplitAmount);

            CreateHover();
        }
    }

    public void ChangeStackText(int i)
    {
        InventoryManager.Instance.SplitAmount += i;

        if (InventoryManager.Instance.SplitAmount < 0)
        {
            InventoryManager.Instance.SplitAmount = 0;
        }
        if (InventoryManager.Instance.SplitAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SplitAmount = InventoryManager.Instance.MaxStackCount;
        }

        InventoryManager.Instance.stackText.text = InventoryManager.Instance.SplitAmount.ToString();
    }

    public void MergeStacks(Slot source, Slot destination)
    {
        int max = destination.currentItem.maxSize - destination.Items.Count;

        int count = source.Items.Count < max ? source.Items.Count : max;

        for (int i = 0; i < count; i++)
        {
            destination.AddItem(source.RemoveItem());
            InventoryManager.Instance.HoverObj.transform.GetChild(0).GetComponent<Text>().text =
                InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString()
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
                        if (!InventoryManager.Instance.MovingSlot.IsEmpty &&
                            InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
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
        if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;
        }
        else if (!InventoryManager.Instance.MovingSlot.IsEmpty)
        {
            Destroy(GameObject.Find("Hover"));

            foreach (Item item in InventoryManager.Instance.MovingSlot.Items)
            {
                InventoryManager.Instance.Clicked.GetComponent<Slot>().AddItem(item);
            }

            InventoryManager.Instance.MovingSlot.ClearSlot();
        }

        InventoryManager.Instance.selectStackSize.SetActive(false);
    }

    /// <summary>
    /// 얼마나 많은 아이템을 지울수 있는지 알려준다
    /// </summary>
    /// <param name="maxStackCount"></param>
    public void SetStackInfo(int maxStackCount)
    {
        //스탁 나누기 위한 UI를 보여준다.
        InventoryManager.Instance.selectStackSize.SetActive(true);

        //오버랩이 되지 않도록 툴팁을 숨겨준다.
        InventoryManager.Instance.toolTipObj.SetActive(false);

        //나눈 아이템을 초기화 한다
        InventoryManager.Instance.SplitAmount = 0;

        //맥스카운트를 저장 한다.
        InventoryManager.Instance.MaxStackCount = maxStackCount;

        //선택된 아이템의 합을 UI에 표시한다.
        InventoryManager.Instance.stackText.text = InventoryManager.Instance.SplitAmount.ToString();
    }

    public void MoveItem(GameObject clicked)
    {

        InventoryManager.Instance.Clicked = clicked;

        if (!InventoryManager.Instance.MovingSlot.IsEmpty)
        {
            Slot tmp = InventoryManager.Instance.Clicked.GetComponent<Slot>();

            if (tmp.IsEmpty)
            {
                tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);
                InventoryManager.Instance.MovingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.IsEmpty && InventoryManager.Instance.MovingSlot.currentItem.itemType == tmp.currentItem.itemType
                && tmp.IsAvailable)
            {
                MergeStacks(InventoryManager.Instance.MovingSlot, tmp);
            }

        }
        else if (InventoryManager.Instance.From == null && canvasGroup.alpha == 1 && !Input.GetKey(KeyCode.LeftShift))
        {
            if (!InventoryManager.Instance.Clicked.GetComponent<Slot>().IsEmpty && !GameObject.Find("Hover"))
            {
                InventoryManager.Instance.From = InventoryManager.Instance.Clicked.GetComponent<Slot>();

                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;

                CreateHover();
            }
        }
        else if (InventoryManager.Instance.To == null && !Input.GetKey(KeyCode.LeftShift))
        {
            InventoryManager.Instance.To = InventoryManager.Instance.Clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }

        //to, from 둘 다 비여있다면
        if (InventoryManager.Instance.From != null && InventoryManager.Instance.To != null)
        {
            //tmpTo에 to를 넣어 준다
            Stack<Item> tmpTo = new Stack<Item>(InventoryManager.Instance.To.Items);
            //to에 from을 넣어 준다
            InventoryManager.Instance.To.AddItems(InventoryManager.Instance.From.Items);

            //tmpTo의 갯수가 0 일때
            if (tmpTo.Count == 0)
            {
                //from에 slot을 지워 준다
                InventoryManager.Instance.From.ClearSlot();
            }
            else
            {
                //tmpTo가 0이 아닐때 from에 tmpTo를 넣어준다
                InventoryManager.Instance.From.AddItems(tmpTo);
            }

            //from에 이미지 컴포넌트를 이용하여 컬러를 화이트로 바꿔준다
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            //to, from을 비워준다
            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
            Destroy(GameObject.Find("Hover"));
            //나름 설정을 해본 것
            //Destroy(hoverObj);
        }
    }

    public void MoveInventory()
    {
        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform,
            new Vector2(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * InventoryManager.Instance.canvas.scaleFactor), Input.mousePosition.y +
            (inventoryRect.sizeDelta.y / 2 * InventoryManager.Instance.canvas.scaleFactor)), InventoryManager.Instance.canvas.worldCamera, out mousePos);

        transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(mousePos);
    }

    public void CreateHover()
    {


        InventoryManager.Instance.HoverObj = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);
        InventoryManager.Instance.HoverObj.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.GetComponent<Image>().sprite;
        InventoryManager.Instance.HoverObj.gameObject.name = "Hover";

        RectTransform hoverTransform = InventoryManager.Instance.HoverObj.GetComponent<RectTransform>();
        RectTransform clickedTransform = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

        InventoryManager.Instance.HoverObj.transform.SetParent(GameObject.Find("Canvas").transform, true);

        InventoryManager.Instance.HoverObj.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObj.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString()
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

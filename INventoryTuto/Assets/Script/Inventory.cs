using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour
{
    #region  Property
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

    ////슬롯 오브젝트 변수
    //public GameObject slotPrefab;

    //슬롯 오브젝트 리스트 변수
    private List<GameObject> allSlots;

    //public Camera camera;

    //public GameObject toolTipObj;

    private float hoverYOffset;

    //비여있는 스롯 갯수 저장 변수
    private static int emptySlot;
    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

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
            return Inventory.instance; 
        }
    }

    private bool isOpen;

    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }

    private bool fadingOut;

    private bool fadingIn;

    public float fadeTime;

    //private GameObject selectStackSizeStatic;

    #endregion

    private static GameObject playerRef;

	// Use this for initialization
	void Start () 
    {

        isOpen = false;

        //selectStackSizeStatic = InventoryManager.Instance.selectStackSize.GetComponent<GameObject>();

        playerRef = GameObject.Find("player");

        CreateLayout();

        canvasGroup = GetComponent<CanvasGroup>();

        InventoryManager.Instance.MovingSlot = GameObject.Find("movingSlot").GetComponent<Slot>();
	}
	
    public void Open()
    {
            if (canvasGroup.alpha > 0)
            {
                StartCoroutine("FadingOut");
                PutItemBack();

                isOpen = false;
            }
            else
            {
                StartCoroutine("FadingIn");

                isOpen = true;
            }
    }

	// Update is called once per frame
	void Update () 
    {


        if (Input.GetMouseButtonUp(0))
        {
            //인벤토리에서 서택된 item을 지운다.
            if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(0) && InventoryManager.Instance.From != null)
            {
                print("eventSystem.IsPointerOverGameObject(-1)");
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;

                foreach (Item item in InventoryManager.Instance.From.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 25;

                    GameObject tmpObj = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpObj.GetComponent<Item>().SetStates(item);
                }
                //from 스롯에서 item을 제거한다
                InventoryManager.Instance.From.ClearSlot();
                Destroy(GameObject.Find("Hover"));

                //오브젝트를 리셋한다
                InventoryManager.Instance.To = null;
                InventoryManager.Instance.From = null;
                emptySlot++;
            }
            else if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && !InventoryManager.Instance.MovingSlot.isEmpty)
            {
                print("eventSystem.IsPointerOverGameObject(-1)");
                foreach (Item item in InventoryManager.Instance.MovingSlot.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 25;

                    GameObject tmpObj = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);

                    tmpObj.GetComponent<Item>().SetStates(item);
                }

                InventoryManager.Instance.MovingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
            }
            //CreateHoverIcon();
        }
        //hoverObject를 체크한다
        if (InventoryManager.Instance.HoverObject != null)
        {
            //hoverObject의 위치변수
            Vector2 position;

            position = (Input.mousePosition - InventoryManager.Instance.canvas.transform.position);

            //position = new Vector2((Input.mousePosition - canvas.transform.localPosition).x, 
            //    (Input.mousePosition - canvas.transform.localPosition).y);
            
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);

            position.Set(position.x, position.y - hoverYOffset);

            InventoryManager.Instance.HoverObject.transform.position = 
                InventoryManager.Instance.canvas.transform.TransformPoint(position);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //
        }

	}

    public void OnDrag()
    {
        MoveInventory();
    }

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        if (!tmpSlot.isEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            InventoryManager.Instance.toolTipObj.SetActive(true);

            

            float xPos = tmpSlot.transform.position.x * slotPaddingLeft;
            float yPos = tmpSlot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;


            InventoryManager.Instance.toolTipObj.transform.position = new Vector2(xPos, yPos);

            InventoryManager.Instance.visualTextObj.text = tmpSlot.currentItem.GetTooltip();
            InventoryManager.Instance.sizeTextObj.text = InventoryManager.Instance.visualTextObj.text;
        }
    }

    public void HideToolTip()
    {
        InventoryManager.Instance.toolTipObj.SetActive(false);
    }

    public void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();

            if (!tmp.isEmpty)
            {
                content += i + "-" + tmp.currentItem.type + "-" + tmp.Items.Count.ToString() + ";";

                print(content);
            }
        }

        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.SetInt(gameObject.name + "slots", slot);
        PlayerPrefs.SetInt(gameObject.name + "rows", rows);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat(gameObject.name + "slotSize", slotSize);
        PlayerPrefs.SetFloat(gameObject.name + "xPos", inventoryRect.position.x);
        PlayerPrefs.SetFloat(gameObject.name + "yPos", inventoryRect.position.y);

        PlayerPrefs.Save();

        //string loadedString = PlayerPrefs.GetString("content");
    }

    public void LoadInventory()
    {
        string content = PlayerPrefs.GetString(gameObject.name + "content");
        print(content);
        slot = PlayerPrefs.GetInt(gameObject.name + "slots");
        rows = PlayerPrefs.GetInt(gameObject.name + "rows");
        slotPaddingLeft = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingLeft");
        slotPaddingTop = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingTop");
        slotSize = PlayerPrefs.GetFloat(gameObject.name + "slotSize");

        inventoryRect.position = new Vector3(PlayerPrefs.GetFloat(gameObject.name + "xPos"), 
            PlayerPrefs.GetFloat(gameObject.name + "yPos"), inventoryRect.position.z);

        CreateLayout();

        string[] splitContent = content.Split(';');                                              //0-MANA-3

        for (int i = 0; i < splitContent.Length - 1; i++)
        {
            print(splitContent[i]);
        }

        for (int x = 0; x < splitContent.Length - 1; x++)
        {
            string[] splitValues = splitContent[x].Split('-');                                  //0; MANA; 3

            //print(splitValues[x]);

            int index = int.Parse(splitValues[0]);

            //print(splitValues[1]);

            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), splitValues[1], true);       //"MANA"

            int amount = int.Parse(splitValues[2]);                                             //"3"

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
                    case ItemType.WEAPON:
                        allSlots[index].GetComponent<Slot>().AddItem(InventoryManager.Instance.sword.GetComponent<Item>());
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

        //allSlot의 초기화
        allSlots = new List<GameObject>();

        hoverYOffset = slotSize * 0.01f;

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
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);
                //slotRect에 newSlot의 렉트컨포넌트를 넣준다.
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                //newSlot 이름을 Slot이라 정해준다
                newSlot.name = "Slot";
                //newSlot의 부모를 cavas롤 변경해준다
                newSlot.transform.SetParent(this.transform);
                //slotRect의 포지션을 정해준다
                slotRect.localPosition = new Vector3(xPosition, yPosition, 0);
                //사이즈 값을 slotSize로 넣어 준다
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

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
                        if (!InventoryManager.Instance.MovingSlot.isEmpty && InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
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

    private void MoveInventory()
    {
        Vector2 mousePos;

        mousePos = (Input.mousePosition - InventoryManager.Instance.canvas.transform.position);

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, new Vector3(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * InventoryManager.Instance.canvas.scaleFactor),
        //    Input.mousePosition.y + (inventoryRect.sizeDelta.y / 2 * InventoryManager.Instance.canvas.scaleFactor)), InventoryManager.Instance.canvas.worldCamera, out mousePos);

        transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(mousePos);
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
        InventoryManager.Instance.Clicked = clicked;

        

        if (!InventoryManager.Instance.MovingSlot.isEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            print("moving");

            if (tmp.isEmpty)
            {
                tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);
                InventoryManager.Instance.MovingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.isEmpty && InventoryManager.Instance.MovingSlot.currentItem.type == tmp.currentItem.type 
                && tmp.isAvailable)
            {
                MergeStacks(InventoryManager.Instance.MovingSlot, tmp);

                //확인이 필요함!!
                //CreateHoverIcon();
            }
        }

        //from 슬롯이 비여 있고, chest가 열려져 있다면 그리고 왼쪽 시프트키를 누르면
        else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().isOpen
            && Input.GetKeyDown(KeyCode.LeftShift))
        {
            //변수 clicked가 비여있지 않다면
            if (!clicked.GetComponent<Slot>().isEmpty && !GameObject.Find("Hover"))
            {
                //from에 클릭 스롯을 넣어준다
                InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                //from의 이미지 컨포넌트에 컬러르 회색으로 바꾸어 준다
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;

                CreateHoverIcon();
            }
        }
        else if (InventoryManager.Instance.To == null && Input.GetKeyDown(KeyCode.LeftShift))
        {
            print(InventoryManager.Instance.MovingSlot.name);

            InventoryManager.Instance.To = clicked.GetComponent<Slot>();

            Destroy(GameObject.Find("Hover"));
        }

        if (InventoryManager.Instance.To != null && InventoryManager.Instance.From != null)
        {
            Stack<Item> tmpTo = new Stack<Item>(InventoryManager.Instance.To.Items);

            InventoryManager.Instance.To.AddItems(InventoryManager.Instance.From.Items);

            if (tmpTo.Count == 0)
            {
                InventoryManager.Instance.From.ClearSlot();
            }
            else
            {
                InventoryManager.Instance.From.AddItems(tmpTo);
            }

            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;

            InventoryManager.Instance.To = null;
            InventoryManager.Instance.From = null;
            InventoryManager.Instance.HoverObject = null;
            Destroy(GameObject.Find("Hover"));
        }

    }

    private void CreateHoverIcon()
    {
        //하버이미지에 아이콘을 넣어준다.
        InventoryManager.Instance.HoverObject = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);
        InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.GetComponent<Image>().sprite;
        InventoryManager.Instance.HoverObject.name = "Hover";

        RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
        RectTransform clickedTransform = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

        InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

        InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString()
            : string.Empty;
    }

    /// <summary>
    /// 인벤토리에 아이템을 다시 넣어 준다
    /// </summary>
    private void PutItemBack()
    {
        if (InventoryManager.Instance.From != null)
        {
            //아이템을 돌려 놓고 하버아이콘을 지운다
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;
        }

        else if (!InventoryManager.Instance.MovingSlot.isEmpty)
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




    public void SpritStack()
    {
        //셀렉스텍사이즈를 안보이게 한다
        InventoryManager.Instance.selectStackSize.SetActive(false);

        if (InventoryManager.Instance.SplitAmount == InventoryManager.Instance.MaxStackCount)
        {
            MoveItem(InventoryManager.Instance.Clicked);
        }
        else if (InventoryManager.Instance.SplitAmount > 0)
        {
            InventoryManager.Instance.MovingSlot.Items = InventoryManager.Instance.Clicked.GetComponent<Slot>().RemoveItems(InventoryManager.Instance.SplitAmount);

            CreateHoverIcon();
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
            InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count.ToString();
        }
        if (source.Items.Count == 0) 
        {
            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }



    private IEnumerator FadingOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadingIn");

            float startAlpha = canvasGroup.alpha;

            float rate = 1f / fadeTime;

            float progress = 0.0f;

            while (startAlpha < 1.0)
	        {
		        canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
                progress += rate * Time.deltaTime;
                yield return null;
	        }

            canvasGroup.alpha = 0;

            fadingOut = false;
        }
    }

    private IEnumerator FadingIn()
    {
        if (!fadingOut)
        {
            fadingIn = true;
            fadingOut = false;
            StopCoroutine("FadingOut");

            float startAlpha = canvasGroup.alpha;

            float rate = 1f / fadeTime;

            float progress = 0.0f;

            while (startAlpha < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 1;

            fadingIn = false;
        }
    }
 
}

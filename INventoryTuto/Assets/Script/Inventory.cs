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
            return Inventory.instance; 
        }
    }

    private bool fadingOut;
    private bool fadingIn;

    public float fadeTime;

    private static GameObject clicked;

    public GameObject selectStackSize;
    public Text stackText;

    private int splitAmount;
    private int maxStackCount;

    private static Slot movingSlot;

	// Use this for initialization
	void Start () 
    {
        CreateLayout();

        canvasGroup = canvas.GetComponent<CanvasGroup>();

        movingSlot = GameObject.Find("movingSlot").GetComponent<Slot>();
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
                emptySlot++;
            }
        }

        if (hoverObject != null)
        {
            Vector2 position;

            position = (Input.mousePosition - canvas.transform.position);

            //position = new Vector2((Input.mousePosition - canvas.transform.localPosition).x, 
            //    (Input.mousePosition - canvas.transform.localPosition).y);

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);

            position.Set(position.x, position.y - hoverYOffset);

            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (canvasGroup.alpha > 0)
            {
                StartCoroutine("FadingOut");
                PutItemBack();
            }
            else
            {
                StartCoroutine("FadingIn");
            }
        }
	}



    private void CreateLayout()
    {
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
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);

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
        Inventory.clicked = clicked;

        if (!movingSlot.isEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            if (tmp.isEmpty)
            {
                tmp.AddItems(movingSlot.Items);
                movingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.isEmpty && movingSlot.currentItem.type == tmp.currentItem.type && tmp.isAvailable)
            {
                MergeStacks(movingSlot, tmp);
            }
        }

        //from 슬롯이 비여 있고, 캔버스알파의 값이 1 그리고 왼쪽 시프트키를 누르면
        else if (from == null && canvasGroup.alpha == 1 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            //변수 clicked가 비여있지 않다면
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                //from에 클릭 스롯을 넣어준다
                from = clicked.GetComponent<Slot>();
                //from의 이미지 컨포넌트에 컬러르 회색으로 바꾸어 준다
                from.GetComponent<Image>().color = Color.gray;

                CreatHoverIcon();
            }
        }
        else if (to == null && Input.GetKeyDown(KeyCode.LeftShift))
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
            Destroy(GameObject.Find("Hover"));
        }

    }

    private void CreatHoverIcon()
    {
        //하버이미지에 아이콘을 넣어준다.
        hoverObject = (GameObject)Instantiate(iconPrefab);
        hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
        hoverObject.name = "Hover";

        RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
        RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

        hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

        hoverObject.transform.localScale = clicked.gameObject.transform.localScale;

        hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString()
            : string.Empty;
    }

    private void PutItemBack()
    {
        if (from != null)
        {
            Destroy(GameObject.Find("Hover"));
            from.GetComponent<Image>().color = Color.white;
            from = null;
        }
    }

    public void SetStackInfo(int maxStackCount)
    {
        selectStackSize.SetActive(true);
        splitAmount = 0;
        this.maxStackCount = maxStackCount;
        stackText.text = splitAmount.ToString();
    }

    public void SpritStack()
    {
        //셀렉스텍사이즈를 안보이게 한다
        selectStackSize.SetActive(false);

        if (splitAmount == maxStackCount)
        {
            MoveItem(clicked);
        }
        else if (splitAmount > 0)
        {
            movingSlot.Items = clicked.GetComponent<Slot>().RemoveItems(splitAmount);

            CreatHoverIcon();
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

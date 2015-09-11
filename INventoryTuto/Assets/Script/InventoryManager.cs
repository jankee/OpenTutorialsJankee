using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour 
{
    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InventoryManager>();
            }
            return InventoryManager.instance; 
        }
    }

    public GameObject slotPrefab;

    //아이콘 이미지를 등록한다.
    public GameObject iconPrefab;

    //등록된 아이콘이미지를 관리할 스테틱 변수
    private GameObject hoverObject;
    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }

    public GameObject mana;
    public GameObject health;
    public GameObject sword;

    public GameObject dropItem;

    public GameObject toolTipObj;

    public Text sizeTextObj;

    public Text visualTextObj;

    public Canvas canvas;

    /// <summary>
    /// 움직일 아이템의 시작점
    /// </summary>
    private Slot from;
    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    /// <summary>
    /// 움직일 아이템의 도착점
    /// </summary>
    private Slot to;
    public Slot To
    {
        get { return to; }
        set { to = value; }
    }
  

    //클릭된 오브젝트
    private GameObject clicked;
    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }
    
    //픽업된 아이템의 합
    public Text stackText;

    public GameObject selectStackSize;

    private int splitAmount;
    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }

    private int maxStackCount;
    public int MaxStackCount
    {
        get { return maxStackCount; }
        set { maxStackCount = value; }
    }

    private Slot movingSlot;
    public Slot MovingSlot
    {
        get { return movingSlot; }
        set { movingSlot = value; }
    }

    public EventSystem eventSystem;

    /// <summary>
    /// 스탁 인포를 셋팅, 얼만큼의 아이템을 없애수 있는 알수 있다.
    /// </summary>
    /// <param name="maxStackCount"></param>
    public void SetStackInfo(int maxStackCount)
    {
        //splitting a stack을 UI에 보여준다
        selectStackSize.SetActive(true);

        //toolTip을 감춘다
        toolTipObj.SetActive(false);

        //splitAmount를 초기화 한다
        splitAmount = 0;

        //maxStackCount에 저장한다.
        this.maxStackCount = maxStackCount;

        //선택된 아이템의 합산을 적어 준다
        stackText.text = splitAmount.ToString();
    }

}

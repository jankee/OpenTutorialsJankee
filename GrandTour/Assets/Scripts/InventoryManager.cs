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
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance; 
        }
    }

    public GameObject slotPrefab;

    public GameObject iconPrefab;

    private GameObject hoverObj;

    public GameObject HoverObj
    {
        get { return hoverObj; }
        set { hoverObj = value; }
    }

    public GameObject mana, health, weapon;

    public GameObject dropItem;

    //private static GameObject toolTip;

    public GameObject toolTipObj;

    public Text sizeTxtObj;

    public Text visualTxtObj;

    public Canvas canvas;

    private Slot from, to;

    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    public Slot To
    {
        get { return to; }
        set { to = value; }
    }

    private GameObject clicked;

    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }

    /// <summary>
    /// 픽업 된 아이템의 합계
    /// 텍스트는 UI의 한 부분을 나누어서 사용한다.
    /// </summary>
    public Text stackText;

    /// <summary>
    /// stack을 나눌 때 사용하는 UI구성
    /// </summary>
    public GameObject selectStackSize;

    private int splitAmount;

    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }

    /// <summary>
    /// 스탁에서 제거할수 있는 최대 아이템의 합
    /// </summary>
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

}

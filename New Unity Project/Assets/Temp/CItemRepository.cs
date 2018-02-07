using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;                     //미니제이슨 사용

public class Item
{
    public string itemName;     //아이템 이름
    public int str; //힘
    public int dex; //민첩
    public int con; //체력
    public List<int> dropStageNums;

    public Item(string itemName)
    {
        this.itemName = itemName;
        this.str = Random.Range(10, 100);
        this.dex = Random.Range(10, 100);
        this.con = Random.Range(10, 100);
        this.dropStageNums = new List<int>();

        //아이템 드랍 스테이지 번호배열
        for (int i = 0; i < 3; i++)
        {
            dropStageNums.Add(Random.Range(1, 40));
        }
    }
}

public class CItemRepository : MonoBehaviour
{
    private Dictionary<string, Item> _itemDic = new Dictionary<string, Item>();

    public InputField ipItemName;

    public Text _msgText;

    public Text _itemNameText;
    public Text _strText;
    public Text _dexText;
    public Text _conText;
    public Text _dropStageText;

    private Item itemRef;

    // Use this for initialization
    private void Start()
    {
        _msgText.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnItemAddButtonClick()
    {
        string _itemName = ipItemName.text.Trim();
        //아이템 생성
        Item item = new Item(_itemName);
        //딕셔너리에 추가
        _itemDic.Add(_itemName, item);

        StartCoroutine(ToastMessageCoroutine("아이템이 한 개 추가 됨"));
    }

    private IEnumerator ToastMessageCoroutine(string msg)
    {
        _msgText.text = msg;

        yield return new WaitForSeconds(2f);

        _msgText.text = "";
    }

    public void OnItemSearchButtonClick()
    {
        string itemName = ipItemName.text.Trim();

        //bool 키를 가진 값이 있는지 여부 = Dictonary.ContainKey("키")
        //-> 지정한 키를 가진 값이 있는지 체크
        if (!_itemDic.ContainsKey(itemName))
        {
            StartCoroutine(ToastMessageCoroutine("아이템이 존재하지 않음"));
            return;
        }

        itemRef = _itemDic[itemName];

        StartCoroutine(ToastMessageCoroutine("아이템 정보 출력"));

        _itemNameText.text = itemRef.itemName;
        _strText.text = "힘 : " + itemRef.str.ToString();
        _dexText.text = "민첩 : " + itemRef.dex.ToString();
        _conText.text = "체력 : " + itemRef.con.ToString();

        List<int> dNums = itemRef.dropStageNums;

        _dropStageText.text = "드랍위치 : ";
        _dropStageText.text += dNums[0].ToString() + ", ";
        _dropStageText.text += dNums[1].ToString() + ", ";
        _dropStageText.text += dNums[2].ToString();
    }

    public void OnItemDeleteButtonClick()
    {
        if (_itemNameText.text.Length <= 0)
        {
            StartCoroutine(ToastMessageCoroutine("삭제 할 아이템이 없음"));

            return;
        }

        if (!_itemDic.ContainsKey(_itemNameText.text))
        {
            StartCoroutine(ToastMessageCoroutine("삭제 할 아이템이 없음"));

            return;
        }

        if (!_itemDic.Remove(_itemNameText.text.Trim()))
        {
            StartCoroutine(ToastMessageCoroutine("아이템 삭제 실패함"));
            return;
        }

        _itemNameText.text = string.Empty;
        _strText.text = string.Empty;
        _dexText.text = string.Empty;
        _conText.text = string.Empty;

        StartCoroutine(ToastMessageCoroutine("아이템을 삭제 함"));
    }

    public void OnItemDicToJsonStarting()
    {
        //string jsonData = MiniJSON.jsonEncode(_itemDic);

        if (itemRef == null)
        {
            return;
        }

        string jsonData = JsonUtility.ToJson(itemRef);

        print(jsonData);

        Item Jitem = JsonUtility.FromJson<Item>(jsonData);

        print("아이템 이름 : " + Jitem.itemName + "\n아이템 힘 : " + Jitem.str);
    }
}
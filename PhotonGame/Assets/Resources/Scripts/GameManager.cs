using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject playerPrefab;

    public GameObject connectPanel;

    public InputField nameInputField;
    public Text msgText;

    public Button createButton;

    public static bool IsJoinedRoom = false;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        createButton.interactable = IsJoinedRoom;
    }

    public void OnCreatePlayerButtonClick()
    {
        if (nameInputField.text.Length <= 0)
        {
            msgText.text = "이름을 입력해 주세요.";

            return;
        }

        PhotonNetwork.playerName = nameInputField.text;

        float posX = Random.Range(-3f, 3f);
        float posZ = Random.Range(0f, 3f);

        connectPanel.SetActive(false);

        //Instantiate(playerPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);

        //포톤 네트워크 오브젝트를 생성함
        //모든 포튼 네트워크 오브젝에는 PhotonView컴포넌트가 추가되어야 함
        //Resources 폴더의 하위 경로
        GameObject localPlayer = PhotonNetwork.Instantiate("Prefabs/ToonSoldier", new Vector3(posX, 0, posZ), Quaternion.identity, 0);

        FollowCam fCam = Camera.main.GetComponent<FollowCam>();
        fCam.Init(localPlayer.transform);
    }
}
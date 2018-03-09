using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///포톤 네트워크 망구조
///클라이언트 <-> 포톤방...  x n <-> 포톤로비 <-> 포톤 클라우드
///
///포톤 접속 순서
///1. 포톤 클라우스 접속
///2. 포톤 로비생성 및 접속(자동 접속 설정 가능)
///3. 포톤 방 생성 및 접속

public class ConnectManager : MonoBehaviour
{
    //메세지 출력 테스트
    public Text msgText;

    private GameManager gameManager;

    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
        }

        //포튼 클라우드에 접속된 상태가 아니면
        if (!PhotonNetwork.connected)
        {
            //포톤 클라우드 접속 및 로비 접속
            PhotonNetwork.ConnectUsingSettings("v1.01");

            msgText.text = "서버 접속 및 로비 생성을 시도함";
        }
    }

    // Use this for initialization
    private void Start()
    {
        //OnJoinedLobby();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //포톤 이벤트 함수 : 로비 접속이 완료되었을때 호출됨
    public void OnJoinedLobby()
    {
        GameManager.IsJoinedRoom = true;

        msgText.text = "게임 서버 및 로비 생성 완료";

        print("[정보] 포톤 클라우드 및 로비 접속이 완료됨 ");

        //PhotonNetwork.CreateRoom() : 포튼 로비에 방을 생성함
        //방을 생성한 사람은 자동으로 접속 됨
        //PhotonNetwork.JoinRoom() : 생성된 방에 접속함
        //PhotonNetwork.JoinRandomRoom() : 생성된 방들 중에 랜덤하게 접속함

        //PhotonNetwork.JoinOrCreateRoom() : 접속하려는 방이 없으면 생성, 있으면 접속함

        ///MaxPlayers : 최대접속 가능 인원
        ///IsOpen : 접속 가능 여부
        ///IsVisible : 찾기 가능 여부
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 10, IsOpen = true, IsVisible = true },
            TypedLobby.Default);
    }

    //포톤 이벤트 함수 : 포톤 로비에 생성된 방에 접속을 완료함
    public void OnJoinedRoom()
    {
        msgText.text = "[정보] 로비에 생성된 방에 접속함";

        //gameManager.OnCreatePlayerButtonClick();
    }

    //포톤 이벤트 함수 : 포톤 클라우드 접속에 실패했을때 호출됨
    public void OnFailedToConnectToPhoton(DisconnectCause error)
    {
        print("[오류] 포톤 클라우드 접속에 실패함 : " + error.ToString());
    }

    //포톤 이벤트 함수 : 포톤 클라우드 접속에 실패했을때 호출됨
    public void OnPhotonJoinRoomFail(object[] error)
    {
        print("[오류] 포톤 로비에 방 생성을 실패함 : " + error[1].ToString());
    }

    //포톤 이벤트 함수 : 포톤 로비에 생성된 방에 랜덤 접속이 실패함
    public void OnPhotonRandomJoinRoomFail(object[] error)
    {
        print("[오류] 포톤 로비에 생성된 방에 랜덤 접속이 실패함 : " + error[1].ToString());

        //방 생성
    }
}
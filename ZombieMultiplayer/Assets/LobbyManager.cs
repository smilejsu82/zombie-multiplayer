using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Button btn;
    public string nickname;
    
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();   //마스터 서버 접속 요청 
        
        btn.interactable = false;
        btn.onClick.AddListener(() =>
        {
            Debug.Log("룸 접속 요청");
            btn.interactable = false;
            Connect();
        });
    }

    private void Connect()
    {
        //마스터 서버 접속 여부 
        Debug.Log($"IsConnected: {PhotonNetwork.IsConnected}");

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = nickname;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();   //마스터 서버 접속 요청 
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        btn.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"OnJoinedRoom : {PhotonNetwork.CurrentRoom.Name}");

        Debug.Log(PhotonNetwork.IsMasterClient);    //방장
        
        //내가 마스터가 아니라면 , 현재 마스터의 닉네임 출력
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.MasterClient != null)
        {
            Debug.Log($"[{PhotonNetwork.MasterClient.NickName}]님이 입장 했습니다.");    
        }

        //내 입장 메시지 출력 
        Debug.Log($"[{PhotonNetwork.NickName}]님이 입장 했습니다.");
        
        //씬전환 
        //PhotonNetwork.LoadLevel("Main");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"[{newPlayer.NickName}]님이 입장 했습니다.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRandomFailed: {returnCode}, {message}");
        
        //방생성 
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");   
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnCreateRoomFailed : {returnCode}, {message}");
    }
}

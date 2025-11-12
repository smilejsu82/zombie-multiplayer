using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public Button btn;
    
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
        Debug.Log("OnJoinedRoom");

        Debug.Log(PhotonNetwork.IsMasterClient);    //방장 
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

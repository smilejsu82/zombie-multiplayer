using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Pun2Manager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    public static Pun2Manager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();   //마스터 서버 접속 요청
    }

    public void JoinLobby(string nickname)
    {
        PhotonNetwork.NickName = nickname;
        Debug.Log($"닉네임이 설정 되었습니다. : {nickname}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터서버에 접속 했습니다.");
        
        //로비 입장
        EventDispatcher.instance.SendEvent((int)EventEnums.EventType.OnConnectedToMaster);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 입장 했습니다.");
        EventDispatcher.instance.SendEvent((int)EventEnums.EventType.OnJoinedLobby);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"OnRoomListUpdate: {roomList.Count}");
        EventDispatcher.instance.SendEvent((int)EventEnums.EventType.OnRoomListUpdate, roomList);
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
        // if (!PhotonNetwork.IsMasterClient && PhotonNetwork.MasterClient != null)
        // {
        //     Debug.Log($"[{PhotonNetwork.MasterClient.NickName}]님이 입장 했습니다.");    
        // }
        
        //원래는..
        var me = PhotonNetwork.LocalPlayer;
        //Debug.Log(me.NickName);
        //Debug.Log(PhotonNetwork.PlayerList);
        
        foreach (var p in PhotonNetwork.PlayerList)
        {
            if (p == me) continue;
            Debug.Log($"[{p.NickName}]님이 입장 했습니다.");
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

    //플레이어꺼 
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"[{otherPlayer.NickName}]님이 퇴장 했습니다.");
    }

    //내꺼 
    public override void OnLeftRoom()
    {
        Debug.Log($"[{PhotonNetwork.NickName}]님이 방을 나갔습니다.");
    }

    public void CreateRoom()
    {
        Debug.Log("방을 만듭니다.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
}

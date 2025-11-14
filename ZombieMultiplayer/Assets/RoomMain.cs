using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomMain : MonoBehaviour
{
    public UIPlayerList uiPlayerList;
    private List<Player> playerList = new List<Player>();
    public Button leaveButton;

    private void Awake()
    {
        EventDispatcher.instance.AddEventHandler<Player>(
            (int)EventEnums.EventType.OnPlayerEnteredRoom,
            OnPlayerEnteredRoomEvent);
        
        EventDispatcher.instance.AddEventHandler(
                    (int)EventEnums.EventType.OnJoinedRoom,
                    OnJoinedRoomEvent);
        
        leaveButton.onClick.AddListener(() =>
        {
            Pun2Manager.instance.LeaveRoom();
        });
    }

    private void OnJoinedRoomEvent(short eventType)
    {
        var me = PhotonNetwork.LocalPlayer;

        // 나보다 먼저 들어와 있던 사람들 출력
        foreach (var p in PhotonNetwork.PlayerList)
        {
            if (p == me) continue;
            Debug.Log($"[{p.NickName}]님이 입장 했습니다.");
            playerList.Add(p);
        }
        
        
        foreach (var p in playerList)
        {
            Debug.Log($"<color=yellow>{p.NickName}</color>");   //lim, hong
        }
        
        //lim, hong 을 hong, lim으로 만들어버리기 
        playerList.Clear();
        
        Player[] sorted = new Player[PhotonNetwork.PlayerList.Length];
        PhotonNetwork.PlayerList.CopyTo(sorted, 0);

        Array.Sort(sorted, (a, b) =>
        {
            //a가 b보다 앞에 와야 한다면 음수 
            if (a == PhotonNetwork.MasterClient && b != PhotonNetwork.MasterClient) return -1;
            //뒤에 와야하면 양수 
            if(b == PhotonNetwork.MasterClient && a != PhotonNetwork.MasterClient) return 1;
            //같은 0
            return a.ActorNumber.CompareTo(b.ActorNumber);
        });
        
        playerList.AddRange(sorted);
        
        foreach (var p in playerList)
        {
            Debug.Log($"<color=lime>{p.NickName}</color>");   //hong, lim
        }
        
        //클라이언트일경우 이곳에서 UIPlayerItem을 만든다
        uiPlayerList.Init(playerList);

    }

    private void OnPlayerEnteredRoomEvent(short eventType, Player newPlayer)
    {
        Debug.Log($"newPlayer: {newPlayer}");   //남
        playerList.Add(newPlayer);

        foreach (var p in playerList)
        {
            Debug.Log($"<color=yellow>{p.NickName}</color>");   //hong, lim
        }
        
        //마스터일경우 이곳에서 UIPlayerItem을 만든다
        uiPlayerList.Init(playerList);
    }

    private void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer);   //나
        
        playerList.Add(PhotonNetwork.LocalPlayer);
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.RemoveEventHandler((int)EventEnums.EventType.OnPlayerEnteredRoom);
        EventDispatcher.instance.RemoveEventHandler((int)EventEnums.EventType.OnJoinedRoom);
    }
}

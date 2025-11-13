using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomMain : MonoBehaviour
{
    public UIPlayerList uiPlayerList;
    private List<Player> playerList = new List<Player>();

    private void Awake()
    {
        EventDispatcher.instance.AddEventHandler<Player>(
            (int)EventEnums.EventType.OnPlayerEnteredRoom,
            OnPlayerEnteredRoomEvent);
        
        EventDispatcher.instance.AddEventHandler(
                    (int)EventEnums.EventType.OnJoinedRoom,
                    OnJoinedRoomEvent);
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

    }

    private void OnPlayerEnteredRoomEvent(short eventType, Player newPlayer)
    {
        Debug.Log($"newPlayer: {newPlayer}");   //남
        playerList.Add(newPlayer);


        foreach (var p in playerList)
        {
            Debug.Log($"<color=yellow>{p.NickName}</color>");   //hong, lim
        }
    }

    private void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer);   //나
        
        playerList.Add(PhotonNetwork.LocalPlayer);
    }
}

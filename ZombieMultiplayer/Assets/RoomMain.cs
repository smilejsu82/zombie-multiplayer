using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomMain : MonoBehaviour
{
    public UIPlayerList uiPlayerList;

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
        }

    }

    private void OnPlayerEnteredRoomEvent(short eventType, Player newPlayer)
    {
        Debug.Log($"newPlayer: {newPlayer}");   //남 
    }

    private void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer);   //나
    }
}

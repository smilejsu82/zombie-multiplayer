using System;
using Photon.Pun;
using UnityEngine;

public class RoomMain : MonoBehaviour
{
    public UIPlayerList uiPlayerList;

    private void Awake()
    {
        // 방 입장 완료
        EventDispatcher.instance.AddEventHandler(
            (int)EventEnums.EventType.OnJoinedRoom,
            OnJoinedRoomEvent);
    }
    private void OnJoinedRoomEvent(short eventType)
    {
        Debug.Log($"AddEventListeners: {(EventEnums.EventType)eventType}");
        
        //PhotonNetwork.LocalPlayer.NickName
    }
}

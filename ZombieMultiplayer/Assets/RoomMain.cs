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

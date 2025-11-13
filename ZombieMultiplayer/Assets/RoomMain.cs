using System;
using Photon.Pun;
using UnityEngine;

public class RoomMain : MonoBehaviour
{
    public UIPlayerList uiPlayerList;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Debug.Log(PhotonNetwork.LocalPlayer);
        
        Debug.Log(PhotonNetwork.PlayerList.Length);
    }
}

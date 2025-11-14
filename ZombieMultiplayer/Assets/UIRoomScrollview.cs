using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRoomScrollview : MonoBehaviour
{
    public Transform contentParent;         // ScrollView의 Content
    public GameObject roomItemPrefab;      // 방 하나를 표시할 프리팹

    public void Init()
    {
        // 룸 리스트 업데이트 이벤트 구독
        EventDispatcher.instance.AddEventHandler<List<RoomInfo>>(
            (int)EventEnums.EventType.OnRoomListUpdate,
            OnRoomListUpdateEvent);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Clear()
    {
        
        
        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            //MissingReferenceException: The object of type 'UnityEngine.RectTransform' has been destroyed but you are still trying to access it.
            //Your script should either check if it is null or you should not destroy the object.
            Destroy(contentParent.GetChild(i).gameObject);
        }
    }

    public void Refresh(List<RoomInfo> roomList)
    {
        Clear();

        foreach (var info in roomList)
        {
            GameObject itemObj = Instantiate(roomItemPrefab, contentParent);
            var ui = itemObj.GetComponent<UIRoomItem>();
            ui.Setup(info);
        }
    }

    private void OnRoomListUpdateEvent(short eventType,  List<RoomInfo> roomList)
    {
        Debug.Log($"UIRoomScrollview RoomCount: {roomList.Count}");
        Refresh(roomList);
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.RemoveEventHandler<List<RoomInfo>>((int)EventEnums.EventType.OnRoomListUpdate, OnRoomListUpdateEvent);
    }
}
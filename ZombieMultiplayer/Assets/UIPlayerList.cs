using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIPlayerList : MonoBehaviour
{
    public GameObject uiPlayerItemPrefab;

    public void Init(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            GameObject go = Instantiate(uiPlayerItemPrefab, transform);
            var item = go.GetComponent<UIPlayerItem>();
            item.Setup(player.NickName, player.IsMasterClient);
        }
    }
}

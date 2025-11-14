using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class UIPlayerList : MonoBehaviour
{
    public GameObject uiPlayerItemPrefab;

    public void Init(List<Player> players)
    {
        UpdateUI(players);
    }

    public void UpdateUI(List<Player> players)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);   
        }
        
        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            GameObject go = Instantiate(uiPlayerItemPrefab, transform);
            var item = go.GetComponent<UIPlayerItem>();
            item.Setup(player.NickName, player.IsMasterClient);
        }
    }
}

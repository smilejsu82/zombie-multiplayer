using Photon.Pun;
using UnityEngine;

public class GamManager : MonoBehaviour
{
    void Start()
    {
        var initPos = Random.insideUnitSphere * 5f;
        initPos.y = 0;

        PhotonNetwork.Instantiate("Woman", initPos, Quaternion.identity);

    }
}

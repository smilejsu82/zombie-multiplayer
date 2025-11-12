using Photon.Pun;
using UnityEngine;

public class Woman : MonoBehaviourPun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(h, 0, v);
        var q = Quaternion.LookRotation(dir);
        transform.rotation = q;
        transform.Translate(Vector3.forward * 1f * Time.deltaTime);
    }
}

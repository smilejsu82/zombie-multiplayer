using Photon.Pun;
using UnityEngine;

public class Woman : MonoBehaviourPun
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
            return;
            
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(h, 0, v);
        if (dir != Vector3.zero)
        {
            Debug.Log(dir);
            var q = Quaternion.LookRotation(dir);
            transform.rotation = q;
            transform.Translate(Vector3.forward * 2.1f * Time.deltaTime);
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
}

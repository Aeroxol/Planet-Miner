using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRocket : MonoBehaviour
{
    public PlayerManager player;
    public Rigidbody2D playerRigid;
    Renderer myRenderer;
    public GameObject leaveBtn;
    public GameObject shopBtn;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerRigid.position.y > (transform.position.y - myRenderer.bounds.size.y / 2))
        && (Mathf.Abs(playerRigid.position.x - transform.position.x) < (myRenderer.bounds.size.x / 2))
        && !player.flying)
        {
            leaveBtn.SetActive(true);
            shopBtn.SetActive(true);
            if (player.hp < player.maxHp)
            {
                player.RestoreHp(player.maxHp / 10);
            }
        }
        else
        {
            leaveBtn.SetActive(false);
            shopBtn.SetActive(false);
        }
            
    }

    private void FixedUpdate()
    {

    }
}

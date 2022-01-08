using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject dynamitePrefab;
    public Rigidbody2D playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemEffect(int itemCode)
    {
        switch (itemCode)
        {
            case 5:
                playerManager.RestoreHp(30);
                break;
            case 6:
                CreateDynamite();
                break;
            case 7:
                EscapeWarp();
                break;
        }
    }

    void CreateDynamite()
    {
        GameObject temp = Instantiate(dynamitePrefab);
        temp.transform.position = player.transform.position;
    }

    void EscapeWarp()
    {
        playerRigid.position = new Vector2(0, 1);
    }
}

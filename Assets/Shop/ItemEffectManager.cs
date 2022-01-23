using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject dynamitePrefab;
    public GameObject dynamiteB_Prefab;
    public GameObject rocketBombPrefab;
    public Rigidbody2D playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        //playerRigid = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ItemEffect(int itemCode)
    {
        bool success = true;

        switch (itemCode)
        {
            case 14:
                playerManager.StartCoroutine(playerManager.RestoreHpItem(500));
                break;
            case 15:
                playerManager.StartCoroutine(playerManager.RestoreHpItem(1000));
                break;
            case 16:
                playerManager.StartCoroutine(playerManager.RestoreHpItem(2000));
                break;
            case 17:
                CreateDynamite();
                break;
            case 18:
                CreateDynamiteB();
                break;
            case 19:
                CreateRocketBomb();
                break;
            case 20:
                EscapeWarp();
                break;
            case 21:
                if (playerManager.itemProtected)
                    success = false;
                else playerManager.itemProtected = true;
                break;
        }
        return success;
    }

    void CreateDynamite()
    {
        GameObject temp = Instantiate(dynamitePrefab);
        temp.transform.position = player.transform.position;
    }
    void CreateDynamiteB()
    {
        GameObject temp = Instantiate(dynamiteB_Prefab);
        temp.transform.position = player.transform.position;
    }
    void CreateRocketBomb()
    {
        GameObject temp = Instantiate(rocketBombPrefab);
        temp.transform.position = player.transform.position;
        temp.transform.Translate(0,0.2f,0);
    }
    void EscapeWarp()
    {
        playerRigid.position = new Vector2(0, 1);
    }
}

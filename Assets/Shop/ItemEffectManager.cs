using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}

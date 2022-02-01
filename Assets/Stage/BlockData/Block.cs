using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockData data;
    private int hp;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public Rigidbody2D myOre;
    [HideInInspector]
    public Stage stage;
    [HideInInspector]
    public int x, y;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public void SetData(BlockData _data)
    {
        data = _data;
        spriteRenderer.sprite = data.artwork[0];
        hp = data.health;
    }

    public void DecreaseHp(int playerPower)
    {
        hp -= playerPower;
        if (data.artwork.Length > 1)
        {
            if ((hp>300)&&(hp <= 400))
            {
                spriteRenderer.sprite = data.artwork[1];
            }
            if ((hp > 200)&&(hp <= 300))
            {
                spriteRenderer.sprite = data.artwork[2];
            }
            if ((hp > 100)&&(hp <= 200))
            {
                spriteRenderer.sprite = data.artwork[3];
            }
            if ((hp <= 100))
            {
                spriteRenderer.sprite = data.artwork[4];
            }
        }
        if (hp <= 0)
        {
            if (myOre != null) myOre.simulated = true;
            GameManager.Instance.curSaveData.curStageMap[x, y] = -9;
            Destroy(gameObject);
        }
    }
}

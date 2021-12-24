using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockData data;
    private int hp;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            switch (hp)
            {
                case 4:
                    spriteRenderer.sprite = data.artwork[1];
                    break;
                case 3:
                    spriteRenderer.sprite = data.artwork[2];
                    break;
                case 2:
                    spriteRenderer.sprite = data.artwork[3];
                    break;
                case 1:
                    spriteRenderer.sprite = data.artwork[4];
                    break;
            }
        }
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

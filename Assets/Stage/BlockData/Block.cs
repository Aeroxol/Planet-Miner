using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockData data;
    private int maxHp;
    private int hp;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public GameObject myOre;
    [HideInInspector] public GameObject myOreWithBody;

    bool[] isNeighborAlive = { true, true, true, true };
    bool[] isNeighborImmortal = { false, false, false, false };
    Sprite[] mySprites = new Sprite[12];
    RaycastHit2D[] hits;

    [HideInInspector]
    public Stage stage;
    [HideInInspector]
    public int x, y;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if ((data.level > 0) && (data.level < 8))
        {
            mySprites = GameManager.Instance.blockLevel[data.level - 1].spriteType;

            hits = Physics2D.BoxCastAll(transform.position, new Vector2(2, 2), 0, Vector2.zero, 0, LayerMask.GetMask("Block"));

            bool isThereUp = false;
            for (int i = 0; i < hits.Length; i++)
            {
                if (transform.position.x == hits[i].transform.position.x)
                {
                    if (transform.position.y < hits[i].transform.position.y)
                    {
                        isThereUp = true;
                        if (hits[i].transform.gameObject.GetComponent<Block>().hp > 99999)
                            isNeighborImmortal[0] = true;
                    }
                    else
                    {
                        if (hits[i].transform.gameObject.GetComponent<Block>().hp > 99999)
                            isNeighborImmortal[1] = true;
                    }
                }
                else if (transform.position.y == hits[i].transform.position.y)
                {
                    if (transform.position.x > hits[i].transform.position.x)
                    {
                        if (hits[i].transform.gameObject.GetComponent<Block>().hp > 99999)
                            isNeighborImmortal[2] = true;
                    }
                    else
                    {
                        if (hits[i].transform.gameObject.GetComponent<Block>().hp > 99999)
                            isNeighborImmortal[3] = true;
                    }
                }
            }
            if (!isThereUp)
            {
                isNeighborAlive[0] = false;
                StartCoroutine(StartChange(0));
            }
            if (isNeighborImmortal[0] || isNeighborImmortal[1] || isNeighborImmortal[2] || isNeighborImmortal[3])
            {
                StartCoroutine(StartChange(-1));
            }
        }

    }

    public void SetData(BlockData _data)
    {
        data = _data;
        spriteRenderer.sprite = data.artwork[0];
        maxHp = data.health;
        hp = maxHp;
    }

    public void DecreaseHp(int playerPower)
    {
        if (data.invincible) return;

        if ((hp > 99999))
            if (playerPower <= 99999) return;

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
            Block tempBlock;
            hits = Physics2D.BoxCastAll(transform.position, new Vector2(2, 2), 0, Vector2.zero, 0, LayerMask.GetMask("Block"));
            for (int i = 0; i < hits.Length; i++)
            {
                if (transform.position.x == hits[i].transform.position.x)
                {
                    if (transform.position.y > hits[i].transform.position.y)
                    {
                        tempBlock = hits[i].transform.gameObject.GetComponent<Block>();
                        tempBlock.ChangeShape(0);
                    }
                    else
                    {
                        tempBlock = hits[i].transform.gameObject.GetComponent<Block>();
                        tempBlock.ChangeShape(1);
                    }

                }
                else if (transform.position.y == hits[i].transform.position.y)
                {
                    if (transform.position.x < hits[i].transform.position.x)
                    {
                        tempBlock = hits[i].transform.gameObject.GetComponent<Block>();
                        tempBlock.ChangeShape(2);
                    }
                    else
                    {
                        tempBlock = hits[i].transform.gameObject.GetComponent<Block>();
                        tempBlock.ChangeShape(3);
                    }
                }
            }

            if (myOre != null)
            {
                myOreWithBody.SetActive(true);
                Destroy(myOre);
                //myOre.SetActive(false);
            }

            GameManager.Instance.curSaveData.curStageMap[x, y] = -9;
            Destroy(gameObject);
        }
    }

    public void ChangeShape(int where) // -1:none 0:up 1:down 2:left 3:right
    {
        if (hp >= 99999) return; //πŸ¿ß
        if (where != -1) isNeighborAlive[where] = false;

        int index = 0;
        if (isNeighborAlive[0] && !isNeighborImmortal[0]) index += 8;
        if (isNeighborAlive[1] && !isNeighborImmortal[1]) index += 4;
        if (isNeighborAlive[2] && !isNeighborImmortal[2]) index += 2;
        if (isNeighborAlive[3] && !isNeighborImmortal[3]) index += 1;

        if (index < 12)
        {
            spriteRenderer.sprite = mySprites[index];
        }
    }

    IEnumerator StartChange(int where)
    {
        yield return new WaitForSeconds(0.5f);
        ChangeShape(where);
    }
}

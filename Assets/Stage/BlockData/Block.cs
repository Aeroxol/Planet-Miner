using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockData data;
    private int hp;
    private int maxHp;
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer crackSpriteRenderer;
    public bool isSpecial = false;

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

    }

    public void SetData(BlockData _data)
    {
        data = _data;
        spriteRenderer.sprite = data.artwork[0];
        hp = data.health;
        maxHp = data.health;

        if (!data.invincible)
        {
            mySprites = GameManager.Instance.blockLevel[data.level - 1].spriteType;

            if (!data.invincible)
            {
                if (y == 0)
                {
                    isNeighborAlive[0] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x, y + 1] < 0) isNeighborAlive[1] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x - 1, y] < 0) isNeighborAlive[2] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x + 1, y] < 0) isNeighborAlive[3] = false;
                    ChangeShape(-1);
                }
                else if (x != 0)
                {
                    if (GameManager.Instance.curSaveData.curStageMap[x, y - 1] < 0) isNeighborAlive[0] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x, y + 1] < 0) isNeighborAlive[1] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x - 1, y] < 0) isNeighborAlive[2] = false;
                    if (GameManager.Instance.curSaveData.curStageMap[x + 1, y] < 0) isNeighborAlive[3] = false;
                    ChangeShape(-1);
                }
            }
        }
        else
        {
            if(x==0||x== GameManager.Instance.curSaveData.curStageData.width - 1|| y == GameManager.Instance.curSaveData.curStageData.height - 1)
            {
                GameManager.Instance.curSaveData.curStageMap[x, y] = -2;
                isSpecial = true;
            }
            if (GameManager.Instance.curSaveData.curStageMap[x, y] == -2)
            {
                spriteRenderer.sprite = data.artwork[1];
            }
        }

    }

    public void DecreaseHp(int playerPower)
    {
        if (isSpecial) return;

        if ((hp > 99999))
            if (playerPower <= 99999) return;

        hp -= playerPower;

        float ratio = ((float)hp / maxHp)/0.125f;
        if (ratio < 1)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[6];
        else if (ratio < 2)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[5];
        else if (ratio < 3)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[4];
        else if (ratio < 4)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[3];
        else if (ratio < 5)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[2];
        else if (ratio < 6)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[1];
        else if (ratio < 7)
            crackSpriteRenderer.sprite = GameManager.Instance.cracks[0];

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

    /*
    public void SetDefaultSprite()
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
                ChangeShape(0);
            }
            if (isNeighborImmortal[0] || isNeighborImmortal[1] || isNeighborImmortal[2] || isNeighborImmortal[3])
            {
                ChangeShape(-1);
            }
        }
    }
    */
}

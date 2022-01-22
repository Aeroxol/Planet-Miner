using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public float boosterPower;
    public float maxFlySpeed;
    public int digPower;
    public int maxHp;
    public int hp;
    int displayHp;
    public int heatResist;
    public int coldResist;
    public float digDelay = 0.5f;
    public InventoryManager invenManager;
    public Image hpBar;
    public Image tempBar;

    Rigidbody2D rigid2d;
    Vector2 colliderSize;
    RaycastHit2D hit;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool digging = false;
    [HideInInspector] public bool flying = true;

    bool isDigDelay = false;
    float digDelaytimer = 0;

    public Text hpTemp;
    float heatDelay = 1.0f;
    bool isHeatDelay = false;
    float heatDelaytimer = 0;
    float temperature = 0;
    float tempIncreaseRate = 3.0f; // 3 2.75 2.5 2.25 2

    WaitForSeconds decreaseDelay = new WaitForSeconds(0.1f);

    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        colliderSize = GetComponent<BoxCollider2D>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveLeft = true;
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveLeft = false;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveRight = true;
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            moveRight = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveUp = true;
        else if (Input.GetKeyUp(KeyCode.UpArrow))
            moveUp = false;
        if (Input.GetKeyDown(KeyCode.Space))
            digging = true;
        else if (Input.GetKeyUp(KeyCode.Space))
            digging = false;

        if (isDigDelay)
        {
            digDelaytimer += Time.deltaTime;
            if (digDelaytimer > digDelay)
            {
                digDelaytimer = 0;
                isDigDelay = false;
            }
        }

        if (isHeatDelay)
        {
            heatDelaytimer += Time.deltaTime;
            if (heatDelaytimer > heatDelay)
            {
                heatDelaytimer = 0;
                isHeatDelay = false;
            }
        }
        else
        {
            if (temperature > 0)
            {
                if (hp > 0)
                {
                    //hp -= (int)(temperature/2) + 1;
                    StartCoroutine(DecreaseHp((int)(temperature / 2) + 1));
                    if (hp < 0)
                    {
                        hp = 0;
                    }
                }
                    
            }
            isHeatDelay = true;
        }

        if (transform.position.y < 0) temperature = Mathf.Abs(rigid2d.position.y)/tempIncreaseRate;
        else temperature = 0;
        tempBar.fillAmount = temperature/100;

        hpTemp.text = "HP: " + hp.ToString() + " / " + maxHp.ToString();
        hpBar.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        Vector2 myVelocity = new Vector2(0, rigid2d.velocity.y);
        if (moveLeft)
            myVelocity += new Vector2(-speed * Time.deltaTime, 0);
        else if (moveRight)
            myVelocity += new Vector2(speed * Time.deltaTime, 0);
        rigid2d.velocity = myVelocity;

        if (moveUp)
        {
            if (rigid2d.velocity.y < maxFlySpeed)
                rigid2d.AddForce(new Vector2(0, boosterPower * Time.deltaTime), ForceMode2D.Impulse);
        }

        //Debug.DrawRay(transform.position, new Vector3(0, -1, 0), new Color(0, 1, 0));
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Block"));
        flying = true;
        if (hit.collider != null) // �ٴڿ� ��Ҵ��� �˻�
        {
            if (hit.distance - (colliderSize.y / 2) < 0.05f)
            {
                flying = false;
            }
            //Debug.Log(hit.distance);
            //Debug.Log(boxCol2d.bounds.size.y / 2);
        }

        if (digging && !flying && !isDigDelay)
        {
            if (moveLeft) // ���� ä��
            {
                hit = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Block"));
                if (hit.collider != null)
                {
                    if (hit.distance - (colliderSize.x / 2) < 0.2f)
                    {
                        hit.collider.GetComponent<Block>().DecreaseHp(digPower);
                    }
                }
            }
            else if (moveRight) // ������ ä��
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Block"));
                if (hit.collider != null)
                {
                    if (hit.distance - (colliderSize.x / 2) < 0.2f)
                    {
                        hit.collider.GetComponent<Block>().DecreaseHp(digPower);
                    }
                }
            }
            else // �Ʒ��� ä��
            {
                rigid2d.MovePosition(new Vector2(hit.transform.position.x, rigid2d.position.y));
                hit.collider.GetComponent<Block>().DecreaseHp(digPower);
            }
            isDigDelay = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ore"))
        {
            //���� ȹ��
            bool gotItem = invenManager.AddItem(col.transform.parent.gameObject.GetComponent<Ore>().data.itemCode, 1);
            if (gotItem) Destroy(col.transform.parent.gameObject);
        }
    }
 
    public void RestoreHp(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
    }

    void GameOver()
    {

    }

    public IEnumerator RestoreHpItem(int amount)
    {
        for (int i = 0; i < amount/50; i++)
        {
            hp+=50;
            if (hp >= maxHp)
            {
                hp = maxHp;
                break;
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator DecreaseHp(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            hp--;
            yield return decreaseDelay;
        }
    }

    public void LeftBtnDown()
    {
        moveLeft = true;
    }
    public void LeftBtnUp()
    {
        moveLeft = false;
    }
    public void RightBtnDown()
    {
        moveRight = true;
    }
    public void RightBtnUp()
    {
        moveRight = false;
    }
    public void UpBtnDown()
    {
        moveUp = true;
    }
    public void UpBtnUp()
    {
        moveUp = false;
    }
    public void DigBtnDown()
    {
        digging = true;
    }
    public void DigBtnUp()
    {
        digging = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public float boosterPower;
    public float maxFlySpeed;
    public int digPower = 50;
    public int maxHp;
    public int hp;
    int radationResist = 0;
    //int displayHp;
    //public int heatResist;
    //public int coldResist;
    UpgradeInfo upgradeInfo;
    public float digDelay = 0.1f;
    public InventoryManager invenManager;
    public Image hpBar;
    public Image tempBar;
    public LeaveStage leaveStage;
    //[HideInInspector] public bool itemProtected = false;
    public Animator animator;
    public GameObject digEffect;
    public GameObject gameOverCanvas;
    public Image blackOut;

    Rigidbody2D rigid2d;
    BoxCollider2D boxCol2d;
    [HideInInspector] public Vector2 colliderSize;
    RaycastHit2D hit;

    float leftRightBound;//�� �¿�����
    float boundPadding = 0;
    float maxHeight;//�� ��������

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool digging = false;
    bool diggingGround = false;
    [HideInInspector] public bool flying = true;

    bool isDigDelay = false;
    float digDelaytimer = 0;

    public Text hpTemp;
    float heatDelay = 1.0f;
    bool isHeatDelay = false;
    float heatDelaytimer = 0;
    float temperature = 0;
    float tempIncreaseRate = 3.0f; // 3 2.75 2.5 2.25 2
    bool playerPaused = false;
    [HideInInspector] public bool canUseItem = true;

    WaitForSeconds decreaseDelay = new WaitForSeconds(0.1f);

    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
        colliderSize = boxCol2d.bounds.size;
        upgradeInfo = GameManager.Instance.upgradeInfo;
        leftRightBound = (GameManager.Instance.curSaveData.curStageData.width - colliderSize.x) / 2;
        maxHeight = 20.0f - colliderSize.y / 2;
        if (GameManager.Instance.curSaveData.curStageData.width % 2 == 0) boundPadding = 0.5f;
        SetStats();
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

        //FOR ANIMATION////////////////////////////////////////
        if (moveLeft && moveRight)
        {
            animator.SetBool("isWalking", false);
        }
        else if (moveLeft && !moveRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (!flying) animator.SetBool("isWalking", true);
        }
        else if (!moveLeft && moveRight)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (!flying) animator.SetBool("isWalking", true);
        }
        else if (!moveLeft && !moveRight) 
        {
            animator.SetBool("isWalking", false);
        }

        if (digging && !flying)
        {
            animator.SetBool("isDigging", true);
            if (moveLeft)
            {
                digEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                digEffect.transform.localEulerAngles = Vector3.zero;
                digEffect.transform.position = new Vector3(rigid2d.position.x - 0.6f, rigid2d.position.y, digEffect.transform.position.z);
            }
            else if (moveRight)
            {
                digEffect.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                digEffect.transform.localEulerAngles = Vector3.zero;
                digEffect.transform.position = new Vector3(rigid2d.position.x + 0.6f, rigid2d.position.y, digEffect.transform.position.z);
            }
            else
            {
                digEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                digEffect.transform.localEulerAngles = new Vector3(0,0,90);
                digEffect.transform.position = new Vector3(rigid2d.position.x, rigid2d.position.y - 0.4f, digEffect.transform.position.z);
            }

        }
        else
        {
            animator.SetBool("isDigging", false);
        }

        if (moveUp)
            animator.SetBool("isFlying", true);
        else animator.SetBool("isFlying", false);

        if (diggingGround) digEffect.SetActive(true);
        else digEffect.SetActive(false);
        ///////////////////////////////////////////////////////

        if (isDigDelay)
        {
            digDelaytimer += Time.deltaTime;
            if (digDelaytimer > digDelay)
            {
                digDelaytimer = 0;
                isDigDelay = false;
            }
        }

        if (playerPaused)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;

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
        }

        if (transform.position.y < 0) temperature = Mathf.Abs(rigid2d.position.y)/tempIncreaseRate;
        else temperature = 0;
        tempBar.fillAmount = temperature/100;

        hpTemp.text = "HP: " + hp.ToString() + " / " + maxHp.ToString();
        hpBar.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    private void FixedUpdate()
    {
        Vector2 myVelocity = new Vector2(0, rigid2d.velocity.y);
        if (moveLeft && !moveRight && !digging && (rigid2d.position.x > -leftRightBound - boundPadding))
            myVelocity += new Vector2(-speed * Time.deltaTime, 0);
        else if (moveRight && !moveLeft && !digging && (rigid2d.position.x < leftRightBound - boundPadding))
            myVelocity += new Vector2(speed * Time.deltaTime, 0);
        rigid2d.velocity = myVelocity;

        //���̵� ���� ä���� �ݶ��̴� �Ͻ� ����
        if (digging && (moveLeft ^ moveRight))
        {
            boxCol2d.offset = new Vector2(-0.25f, 0);
        }
        else
        {
            boxCol2d.offset = new Vector2(0, 0);
        }

        if (moveUp)
        {
            if (rigid2d.velocity.y < maxFlySpeed)
                rigid2d.AddForce(new Vector2(0, boosterPower * Time.deltaTime), ForceMode2D.Impulse);
        }
        if (rigid2d.position.y >= maxHeight)
        {
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, 0);
            rigid2d.MovePosition(new Vector2(rigid2d.position.x, maxHeight));
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

        diggingGround = false;
        if (digging && !flying)
        {
            if (moveLeft) // ���� ä��
            {
                hit = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Block"));
                if (hit.collider != null)
                {
                    if (hit.distance - (colliderSize.x / 2) < 0.3f)
                    {
                        diggingGround = true;
                        if(!isDigDelay) hit.collider.GetComponent<Block>().DecreaseHp(digPower);
                    }
                }
            }
            else if (moveRight) // ������ ä��
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Block"));
                if (hit.collider != null)
                {
                    if (hit.distance - (colliderSize.x / 2) < 0.3f)
                    {
                        diggingGround = true;
                        if (!isDigDelay) hit.collider.GetComponent<Block>().DecreaseHp(digPower);
                    }
                }
            }
            else // �Ʒ��� ä��
            {
                diggingGround = true;
                rigid2d.MovePosition(new Vector2(hit.transform.position.x, rigid2d.position.y));
                if(!isDigDelay) hit.collider.GetComponent<Block>().DecreaseHp(digPower);
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

    IEnumerator GameOver()
    {
        gameOverCanvas.SetActive(true);
        animator.SetTrigger("die");

        digging = false;
        moveLeft = false;
        moveRight = false;
        moveUp = false;

        while (true)
        {
            blackOut.color = new Color(0, 0, 0, blackOut.color.a + 0.0004f * Time.deltaTime);
            if (blackOut.color.a >= 0.9f)
                break;
            yield return null;
        }

        leaveStage.GameOver(GameManager.Instance.curSaveData.itemProtected);
    }

    void SetStats()
    {
        digPower = upgradeInfo.digPowerList[GameManager.Instance.curSaveData.myUpgradeLvs[0] - 1];
        boosterPower = upgradeInfo.boosterPowerList[GameManager.Instance.curSaveData.myUpgradeLvs[1] - 1];
        maxFlySpeed = upgradeInfo.maxFlyList[GameManager.Instance.curSaveData.myUpgradeLvs[1] - 1];
        maxHp = upgradeInfo.hpAmountList[GameManager.Instance.curSaveData.myUpgradeLvs[2] - 1];
        hp = maxHp;
        radationResist = upgradeInfo.resistAmountList[GameManager.Instance.curSaveData.myUpgradeLvs[4] - 1];
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

    public void PausePlayer(bool stop)
    {
        if (stop)
        {
            rigid2d.simulated = false;
            playerPaused = true;
        }
        else
        {
            rigid2d.simulated = true;
            playerPaused = false;
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

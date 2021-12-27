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
    public int heatResist;
    public int coldResist;
    public float digDelay = 0.5f;

    Rigidbody2D rigid2d;
    BoxCollider2D boxCol2d;
    Vector2 colliderSize;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveUp = false;
    bool digging = false;
    bool flying = true;

    bool isDigDelay = false;
    float digDelaytimer = 0;

    public Text hpTemp;
    float heatDelay = 1.0f;
    bool isHeatDelay = false;
    float heatDelaytimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
        colliderSize = boxCol2d.bounds.size;
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

        hpTemp.text = "HP: " + hp.ToString() + " / " + maxHp.ToString();
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
            if (transform.position.y < 0)
            {
                if (hp > 0)
                    hp -= 1;
            }
            else
            {
                if(hp<maxHp)
                    hp += hp/5;
                if (hp > maxHp) hp = maxHp;
            } 
            isHeatDelay = true;
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
    }


    private void OnCollisionStay2D(Collision2D col)
    {
        if (flying) return;

        if (col.collider.CompareTag("Block"))
        {
            if (!isDigDelay && digging)
            {
                if (moveLeft)
                {
                    if (col.transform.position.x < transform.position.x)
                    {
                        if (Mathf.Abs(col.transform.position.y - transform.position.y) < colliderSize.y / 2)
                        {
                            isDigDelay = true;
                            col.gameObject.GetComponent<Block>().DecreaseHp(digPower);
                        }
                    }
                }
                else if (moveRight)
                {
                    if (col.transform.position.x > transform.position.x)
                    {
                        if (Mathf.Abs(col.transform.position.y - transform.position.y) < colliderSize.y / 2)
                        {
                            isDigDelay = true;
                            col.gameObject.GetComponent<Block>().DecreaseHp(digPower);
                        }
                    }
                }
                else
                {
                    if (col.transform.position.y < transform.position.y)
                    {
                        if (Mathf.Abs(col.transform.position.x - transform.position.x) < colliderSize.x / 2)
                        {
                            rigid2d.MovePosition(new Vector2(col.transform.position.x, rigid2d.position.y));
                            isDigDelay = true;
                            col.gameObject.GetComponent<Block>().DecreaseHp(digPower);
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Block"))
        {   //공중에 떠있는지 감지
            if ((col.transform.position.y + col.collider.bounds.size.y/2) < (transform.position.y - boxCol2d.bounds.size.y/2))
            {
                if (Mathf.Abs(col.transform.position.x - transform.position.x) < (col.collider.bounds.size.x / 2 + boxCol2d.bounds.size.x / 2))
                {
                    flying = false;
                    //Debug.Log("Enter");
                }
            }
        }
        else if (col.collider.CompareTag("Ore"))
        {   //광석 획득
            col.gameObject.SetActive(false);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {   
        if (col.collider.CompareTag("Block"))
        {   //공중에 떠있는지 감지
            if ((col.transform.position.y + col.collider.bounds.size.y / 2) < (transform.position.y - boxCol2d.bounds.size.y / 2))
            {
                if (Mathf.Abs(col.transform.position.x - transform.position.x) < (col.collider.bounds.size.x / 2 + boxCol2d.bounds.size.x / 2))
                {
                    flying = true;
                    //Debug.Log("Exit");
                }
            }

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

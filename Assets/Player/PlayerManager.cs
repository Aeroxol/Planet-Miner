using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public float boosterPower;
    public float maxFlySpeed;
    public int attackPower;

    Rigidbody2D rigid2d;
    BoxCollider2D boxCol2d;

    bool moveLeft;
    bool moveRight;
    bool moveUp;

    float atkDelay = 0.5f;
    bool isAtkDelay = false;
    float atkDelaytimer = 0;

    public Button LeftArrow;
    public Button RightArrow;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveLeft = true;
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            moveLeft = false;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            moveRight = true;
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            moveRight = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveUp = true;
        else if (Input.GetKeyUp(KeyCode.UpArrow))
            moveUp = false;

        if (isAtkDelay)
        {
            atkDelaytimer += Time.deltaTime;
            if (atkDelaytimer > atkDelay)
            {
                atkDelaytimer = 0;
                isAtkDelay = false;
            }
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

    /*
    private void OnCollisionStay2D(Collision2D col)
    {
        if (rigid2d.velocity.y != 0) return;

        if (col.gameObject.CompareTag(" "))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if(Input.GetKey(KeyCode.LeftArrow))
                {

                }
                if (Input.GetKey(KeyCode.RightArrow))
                {

                }
                else
                {

                }
            }
        }
    }
    */

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
}

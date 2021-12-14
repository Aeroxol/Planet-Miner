using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            moveLeft = true;
        else if (Input.GetKey(KeyCode.RightArrow))
            moveRight = true;
        if (Input.GetKey(KeyCode.UpArrow))
            moveUp = true;

        if(isAtkDelay)
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
        {
            myVelocity += new Vector2(-speed * Time.deltaTime, 0);
            moveLeft = false;
        }
        else if (moveRight)
        {
            myVelocity += new Vector2(speed * Time.deltaTime, 0);
            moveRight = false;
        }
        rigid2d.velocity = myVelocity;

        if (moveUp)
        {
            if (rigid2d.velocity.y < maxFlySpeed)
            {
                rigid2d.AddForce(new Vector2(0, boosterPower * Time.deltaTime), ForceMode2D.Impulse);
            }
            moveUp = false;
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
}

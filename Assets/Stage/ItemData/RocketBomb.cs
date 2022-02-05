using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBomb : MonoBehaviour
{
    Vector2 speed = new Vector2(0,6);
    RaycastHit2D hit;
    float halfHeight;

    // Start is called before the first frame update
    void Start()
    {
        halfHeight = GetComponent<Renderer>().bounds.size.y / 2;   
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y>20) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime);
        hit = Physics2D.Raycast(transform.position, Vector2.up, halfHeight, LayerMask.GetMask("Block"));
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Block>().DecreaseHp(10000);
            Destroy(gameObject);
        }
    }
}

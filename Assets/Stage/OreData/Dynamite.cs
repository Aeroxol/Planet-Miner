using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    float explosionDelay = 3.0f;
    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;
    RaycastHit2D hit;
    GameObject block;

    public Rigidbody2D rigid2d;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, new Vector3(-0.9f, -0.9f, 0), new Color(0, 1, 0));
        //Debug.DrawRay(transform.position, new Vector3(0.9f, -0.9f, 0), new Color(0, 1, 0));
    }

    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Block"));
        if (hit.collider != null)
            block = hit.collider.gameObject;
        else block = null;

        if (block != null) rigid2d.MovePosition(new Vector2(block.transform.position.x, rigid2d.position.y));
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        if (block != null)
        {
            block.GetComponent<Block>().DecreaseHp(10000);
        }
        Destroy(gameObject);
    }
}

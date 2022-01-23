using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteB : MonoBehaviour
{
    float explosionDelay = 3.0f;
    RaycastHit2D hit;
    RaycastHit2D[] hits;
    GameObject[] blocks = new GameObject[8];

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
            rigid2d.MovePosition(new Vector2(hit.transform.position.x, rigid2d.position.y)); ;

        hits = Physics2D.BoxCastAll(transform.position, Vector2.one, 0, Vector2.zero, 0, LayerMask.GetMask("Block"));
        Debug.Log(hits.Length);
        for (int i = 0; i < 8; i++)
        {
            if (i < hits.Length)
            {
                blocks[i] = hits[i].transform.gameObject;
            }
            else blocks[i] = null;
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        for (int i = 0; i < 8; i++)
        {
            if (blocks[i] != null)
            {
                blocks[i].GetComponent<Block>().DecreaseHp(100000000);
            }
        }
        Destroy(gameObject);
    }
}

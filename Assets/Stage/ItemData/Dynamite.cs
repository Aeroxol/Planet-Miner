using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    float explosionDelay = 3.0f;
    RaycastHit2D[] hits;
    GameObject block;
    List<GameObject> blocks = new List<GameObject>();
    Vector3 positionSetOrigin;

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
        positionSetOrigin.Set(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        hits = Physics2D.BoxCastAll(positionSetOrigin, new Vector2(0.6f, 0.1f), 0, Vector2.zero, 0, LayerMask.GetMask("Block"));
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.position.y + 0.5f < transform.position.y)
            {
                blocks.Add(hits[i].transform.gameObject);
            }
        }

        if (hits.Length == 1) block = hits[0].transform.gameObject;
        else if (hits.Length == 2)
        {
            if (Mathf.Abs(transform.position.x - hits[0].transform.position.x) < Mathf.Abs(transform.position.x - hits[1].transform.position.x))
                block = hits[0].transform.gameObject;
            else block = hits[1].transform.gameObject;
        }
        else block = null;

        if (block != null) rigid2d.MovePosition(new Vector2(block.transform.position.x, rigid2d.position.y));
        blocks.Clear();
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        if (block != null)
        {
            block.GetComponent<Block>().DecreaseHp(100000000);
        }
        Destroy(gameObject);
    }
}

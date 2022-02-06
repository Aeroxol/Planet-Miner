using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteB : MonoBehaviour
{
    float explosionDelay = 3.0f;
    RaycastHit2D hit;
    RaycastHit2D[] hits;
    GameObject block;
    List<GameObject> blocksUnderMe = new List<GameObject>();
    GameObject[] blocks = new GameObject[8];
    Vector3 positionSetOrigin;

    public Rigidbody2D rigid2d;
    [HideInInspector] public GameObject explosionEffectPrefab;

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
        positionSetOrigin.Set(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        hits = Physics2D.BoxCastAll(positionSetOrigin, new Vector2(0.7f, 0.2f), 0, Vector2.zero, 0, LayerMask.GetMask("Block"));
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.position.y + 0.5f < transform.position.y)
            {
                blocksUnderMe.Add(hits[i].transform.gameObject);
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
        blocksUnderMe.Clear();

        hits = Physics2D.BoxCastAll(transform.position, Vector2.one, 0, Vector2.zero, 0, LayerMask.GetMask("Block"));
        //Debug.Log(hits.Length);
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
        GameObject temp = Instantiate(explosionEffectPrefab);
        temp.transform.position = transform.position;
        temp.transform.localScale = new Vector3(3f, 3f, 3f);
        temp.SetActive(true);
        Destroy(gameObject);
    }
}

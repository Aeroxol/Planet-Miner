using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    float explosionDelay = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Block"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, new Vector2(-1,-1), 0.9f, LayerMask.GetMask("Block"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, new Vector2(1, -1), 0.9f, LayerMask.GetMask("Block"));

        if (hitLeft.collider != null)
        {
            if (hitRight.collider != null)
            {
                if (Vector2.Distance(transform.position, hitLeft.transform.position) <= Vector2.Distance(transform.position, hitRight.transform.position))
                    StartCoroutine(Explosion(hitLeft.collider.GetComponent<Block>()));
                else
                    StartCoroutine(Explosion(hitRight.collider.GetComponent<Block>()));
            }
            else
                StartCoroutine(Explosion(hitLeft.collider.GetComponent<Block>()));
        }
        else
        {
            if (hitRight.collider != null)
                StartCoroutine(Explosion(hitRight.collider.GetComponent<Block>()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, new Vector3(-0.9f, -0.9f, 0), new Color(0, 1, 0));
        //Debug.DrawRay(transform.position, new Vector3(0.9f, -0.9f, 0), new Color(0, 1, 0));
    }

    IEnumerator Explosion(Block block)
    {
        transform.position = new Vector2(block.gameObject.transform.position.x, transform.position.y);
        yield return new WaitForSeconds(explosionDelay);
        block.DecreaseHp(10000);
        Destroy(gameObject);
    }
}

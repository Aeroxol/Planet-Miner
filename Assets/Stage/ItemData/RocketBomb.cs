using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBomb : MonoBehaviour
{
    Vector2 speed = new Vector2(0,6);
    RaycastHit2D hit;
    float halfHeight;

    [HideInInspector] public GameObject explosionEffectPrefab;
    [HideInInspector] public PlayerManager playerManager;

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
        if(!playerManager.playerPaused) transform.Translate(speed * Time.deltaTime);
        hit = Physics2D.Raycast(transform.position, Vector2.up, halfHeight, LayerMask.GetMask("Block"));
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Block>().DecreaseHp(10000);
            SoundManager.Play("explosion1");
            GameObject temp = Instantiate(explosionEffectPrefab);
            temp.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            temp.SetActive(true);
            Destroy(gameObject);
        }
    }
}

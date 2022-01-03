using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public OreData data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(OreData _data)
    {
        data = _data;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.artwork;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Block"))
            GetComponent<Rigidbody2D>().isKinematic = false;
    }
}

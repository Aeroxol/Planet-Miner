using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockData data;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(BlockData _data)
    {
        data = _data;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.artwork;
        hp = data.health;
    }
}

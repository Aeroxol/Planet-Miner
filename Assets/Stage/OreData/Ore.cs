using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public OreData data;

    public void SetData(OreData _data)
    {
        data = _data;
        gameObject.GetComponent<SpriteRenderer>().sprite = data.artwork;
    }
}

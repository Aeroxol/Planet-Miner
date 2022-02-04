using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFlame : MonoBehaviour
{
    bool shrink = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = Time.deltaTime * 6f;

        if (shrink)
        {
            transform.localScale = new Vector3(transform.localScale.x - ratio, transform.localScale.y - ((ratio / 3) * 4), transform.localScale.z);

            if (transform.localScale.x <= 0.3f)
            {
                transform.localScale = new Vector3(0.3f, 0.6f, 1);
                shrink = false;
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + ratio, transform.localScale.y + ((ratio / 3) * 4), transform.localScale.z);
            if (transform.localScale.x >= 0.9f)
            {
                transform.localScale = new Vector3(0.9f, 1.2f, 1);
                shrink = true;
            }
        }
    }
}

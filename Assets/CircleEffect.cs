using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleEffect : MonoBehaviour
{
    public SpriteRenderer effect;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, effect.color.a - Time.deltaTime*0.2f);
        effect.transform.localScale = new Vector3(effect.transform.localScale.x + Time.deltaTime * 2f, effect.transform.localScale.y + Time.deltaTime * 2f, effect.transform.localScale.z);
        if (effect.color.a <= 0)
        {
            Destroy(gameObject);
            //effect.color = new Color(effect.color.r, effect.color.g, effect.color.b, 1);
            //effect.transform.localScale = new Vector3(0.1f,0.1f, effect.transform.localScale.z);
        }
        transform.position = transform.parent.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBackground : MonoBehaviour
{
    float moveTimer = 30;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= 30)
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    x = 0.5f;
                    y = 0.5f;
                    break;
                case 1:
                    x = -0.5f;
                    y = 0.5f;
                    break;
                case 2:
                    x = 0.5f;
                    y = -0.5f;
                    break;
                case 3:
                    x = -0.5f;
                    y = -0.5f;
                    break;
            }
            moveTimer = 0;
        }

        transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);

        if (Mathf.Abs(transform.position.x) >= 15) x = -x;
        if (Mathf.Abs(transform.position.y) >= 15) y = -y;
    }
}
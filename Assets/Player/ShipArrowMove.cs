using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipArrowMove : MonoBehaviour
{
    Vector2 originalPos;
    Vector2 targetPos;

    bool goToTarget = true;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        targetPos = new Vector3(originalPos.x + 10.0f, originalPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (goToTarget)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + Time.deltaTime*30.0f, targetPos.y);
            if (rectTransform.anchoredPosition.x >= targetPos.x)
            {
                rectTransform.anchoredPosition = new Vector3(targetPos.x, targetPos.y);
                goToTarget = false;
            }

        }
        else
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x - Time.deltaTime*30.0f, originalPos.y);
            if (rectTransform.anchoredPosition.x <= originalPos.x)
            {
                rectTransform.anchoredPosition = new Vector3(originalPos.x, originalPos.y);
                goToTarget = true;
            }
        }
    }
}

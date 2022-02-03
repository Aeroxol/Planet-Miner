using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 targetPosition = new Vector3();
    Vector3 myPosition;

    Camera my;
    int stageWidth;
    int stageHeight;
    float myX = 0;
    float myY = 0;

    private void Start()
    {
        my = GetComponent<Camera>();
        stageWidth = GameManager.Instance.curSaveData.curStageData.width;
        stageHeight = GameManager.Instance.curSaveData.curStageData.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, target.transform.position.z);

            if (Mathf.Abs(targetPosition.x + 0.5f) < (stageWidth / 2) - (my.orthographicSize * my.aspect))
                myX = targetPosition.x;
            else myX = transform.position.x;

            if ((targetPosition.y < 40 - my.orthographicSize) && (targetPosition.y > my.orthographicSize - stageHeight + 0.5f))
                myY = targetPosition.y;
            else myY = transform.position.y;

            myPosition.Set(myX, myY, transform.position.z);
            transform.position = myPosition;
        }
    }
}

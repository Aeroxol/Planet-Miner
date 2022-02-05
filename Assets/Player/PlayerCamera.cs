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
    bool isEven = true;
    float oddPadding = 0;
    float evenPadding = 0;

    private void Start()
    {
        my = GetComponent<Camera>();
        stageWidth = GameManager.Instance.curSaveData.curStageData.width;
        stageHeight = GameManager.Instance.curSaveData.curStageData.height;
        if (stageWidth % 2 == 1) {
            oddPadding = 0.5f;
            isEven = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, target.transform.position.z);

            if (isEven)
            {
                if (targetPosition.x > 0) evenPadding = -0.5f;
                else evenPadding = 0.5f;
            }

            if (Mathf.Abs(targetPosition.x) < (stageWidth / 2 + oddPadding + evenPadding) - (my.orthographicSize * my.aspect))
                myX = targetPosition.x;
            else if (targetPosition.x < 0) myX = -(stageWidth / 2 + oddPadding + evenPadding) + (my.orthographicSize * my.aspect);
            else myX = (stageWidth / 2 + oddPadding + evenPadding) - (my.orthographicSize * my.aspect);

            if ((targetPosition.y < 20 - my.orthographicSize) && (targetPosition.y > my.orthographicSize - stageHeight + 0.5f))
                myY = targetPosition.y;
            else if (targetPosition.y > 0) myY = 20 - my.orthographicSize;
            else myY = my.orthographicSize - stageHeight + 0.5f;

            myPosition.Set(myX, myY, transform.position.z);
            transform.position = myPosition;
        }
    }

    public void zoomOutBtnDown()
    {
        my.orthographicSize = 10;
    }
    public void zoomOutBtnUp()
    {
        my.orthographicSize = 5;
    }
}

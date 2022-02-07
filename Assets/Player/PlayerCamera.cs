using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject zoomCool_Img;
    public GameObject coolBarBackground;
    public Image coolBarFill;
    public Text zoomCoolTxt;
    //public GameObject circleEffectPrefab;
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

    readonly float zoomTime = 3.0f;
    float zoomTimeCount = 0;
    float zoomCool = 5.0f;
    float zoomCoolCount = 0;
    bool isZoomOut = false;
    bool isZoomCool = false;
    bool zoomBtnLock = false;
    //bool createEffect = false;

    //readonly float effectDelay = 1.0f;
    //float effectDelayCount = 0;

    readonly float defaultSize = 5.5f;

    private void Start()
    {
        my = GetComponent<Camera>();
        stageWidth = GameManager.Instance.curSaveData.curStageData.width;
        stageHeight = GameManager.Instance.curSaveData.curStageData.height;
        if (stageWidth % 2 == 1) {
            oddPadding = 0.5f;
            isEven = false;
        }
        switch (GameManager.Instance.curSaveData.curStageData.level)
        {
            case 0:
                zoomCool = 5.0f;
                break;
            case 1:
                zoomCool = 10.0f;
                break;
            case 2:
                zoomCool = 14.0f;
                break;
            case 3:
                zoomCool = 17.0f;
                break;
            case 4:
                zoomCool = 20.0f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isZoomOut)
        {
            zoomTimeCount += Time.deltaTime;
            coolBarFill.fillAmount = 1.0f - (zoomTimeCount / zoomTime);
            if (zoomTimeCount >= zoomTime)
            {
                zoomTimeCount = 0;
                isZoomOut = false;
                coolBarBackground.SetActive(false);
                //createEffect = false;
                //effectDelayCount = 0;
                StartCoroutine(ZoomIn());
            }
        }
        if (isZoomCool)
        {
            zoomCoolCount += Time.deltaTime;
            if (zoomCool - zoomCoolCount < 10)
                zoomCoolTxt.text = string.Format("{0:N1}", zoomCool - zoomCoolCount);
            else zoomCoolTxt.text = Mathf.Round(zoomCool - zoomCoolCount).ToString();
            
            if (zoomCoolCount >= zoomCool)
            {
                zoomCool_Img.SetActive(false);
                zoomCoolCount = 0;
                isZoomCool = false;
            }
        }
        /*
        if (createEffect)
        {
            effectDelayCount += Time.deltaTime;
            if (effectDelayCount >= effectDelay)
            {
                GameObject temp = Instantiate(circleEffectPrefab);
                temp.transform.SetParent(target.transform);
                temp.SetActive(true);
                effectDelayCount = 0;
            }
        }
        */


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

    IEnumerator ZoomOut()
    {
        //createEffect = true;
        while (true)
        {
            my.orthographicSize += Time.deltaTime * 4;
            if (my.orthographicSize >= 10)
            {
                my.orthographicSize = 10;
                break;
            }
            yield return null;
        }
        isZoomOut = true;
        coolBarBackground.SetActive(true);
    }
    IEnumerator ZoomIn()
    {
        while (true)
        {
            my.orthographicSize -= Time.deltaTime * 4;
            if (my.orthographicSize <= defaultSize)
            {
                my.orthographicSize = defaultSize;
                break;
            }
            yield return null;
        }
        isZoomCool = true;
        zoomCoolTxt.text = zoomCool.ToString();
        zoomCool_Img.SetActive(true);
        zoomBtnLock = false;
    }

    public void zoomOutClick()
    {
        if (zoomBtnLock) return;

        if (!isZoomCool && !isZoomOut)
        {
            StartCoroutine(ZoomOut());
        }
        else if (!isZoomCool && isZoomOut)
        {
            zoomTimeCount = zoomTime;
            zoomBtnLock = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipUpgradeManager : MonoBehaviour
{
    UpgradeInfo upgradeInfo;

    public GameObject shipUpgradePanel;
    public InventoryManager im;
    public GameObject barrier;

    public Text currentLvTxt;
    public Text lvDescription;
    public GameObject materialText;
    public GameObject maxLvText;
    public GameObject openPushBtnA;
    public GameObject openPushBtnB;
    public GameObject openPushBtnC;
    public GameObject openPushBtnD;
    public Image[] thumbnails;
    public Text[] names;
    public Text[] amounts;

    public GameObject putPanel;
    public Text requireTxt;
    public Text myAmountTxt;
    public Text amountTxt;
    int requireMaAmount; //필요한 자원량
    int myMaAmount; //가지고있는 자원량
    int curMaAmount; //넣으려고 한는 자원량
    int curLv;
    int curItemCode;
    int curMaterialIndex; //4개의 필요자원 중 몇번째인가(0~3)

    public TextMeshProUGUI shipLvTmp;

    // Start is called before the first frame update
    void Start()
    {
        upgradeInfo = GameManager.Instance.upgradeInfo;
        if(shipLvTmp!=null) shipLvTmp.text = "Lv " + (GameManager.Instance.curSaveData.myShipLv + 1);
        SetPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPanel()
    {
        curLv = GameManager.Instance.curSaveData.myShipLv + 1;
        currentLvTxt.text = "우주선 레벨: " + curLv.ToString();
        lvDescription.text = curLv.ToString() + "단계 행성까지 탐사할 수 있다.";

        if (curLv >= 5)
        {
            materialText.SetActive(false);
            maxLvText.SetActive(true);
            openPushBtnA.SetActive(false);
            openPushBtnB.SetActive(false);
            openPushBtnC.SetActive(false);
            openPushBtnD.SetActive(false);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                thumbnails[i].sprite = im.itemData[upgradeInfo.materialType[curLv - 1, i]].artwork;
                names[i].text = im.itemData[upgradeInfo.materialType[curLv - 1, i]].itemName.ToString();
                amounts[i].text = GameManager.Instance.curSaveData.myShipMaterials[i].ToString()
                    + "/" + upgradeInfo.materialAmount[curLv - 1, i].ToString();
            }
        }
    }

    void OpenPutPanel()
    {
        curLv = GameManager.Instance.curSaveData.myShipLv + 1;
        curItemCode = upgradeInfo.materialType[curLv - 1, curMaterialIndex];

        requireMaAmount = upgradeInfo.materialAmount[curLv - 1, curMaterialIndex] - GameManager.Instance.curSaveData.myShipMaterials[curMaterialIndex];
        myMaAmount = im.itemTotal[curItemCode];
        curMaAmount = 0;

        requireTxt.text = requireMaAmount.ToString();
        myAmountTxt.text = myMaAmount.ToString();
        amountTxt.text = curMaAmount.ToString();

        putPanel.SetActive(true);
    }

    public void OpenPutClick_1()
    {
        if (GameManager.Instance.curSaveData.myShipMaterials[0] >= upgradeInfo.materialAmount[curLv-1, 0]) return;

        curMaterialIndex = 0;
        OpenPutPanel();
    }
    public void OpenPutClick_2()
    {
        if (GameManager.Instance.curSaveData.myShipMaterials[1] >= upgradeInfo.materialAmount[curLv-1, 1]) return;
        curMaterialIndex = 1;
        OpenPutPanel();
    }
    public void OpenPutClick_3()
    {
        if (GameManager.Instance.curSaveData.myShipMaterials[2] >= upgradeInfo.materialAmount[curLv-1, 2]) return;
        curMaterialIndex = 2;
        OpenPutPanel();
    }
    public void OpenPutClick_4()
    {
        if (GameManager.Instance.curSaveData.myShipMaterials[3] >= upgradeInfo.materialAmount[curLv-1, 3]) return;
        curMaterialIndex = 3;
        OpenPutPanel();
    }

    public void UpgradeClick()
    {
        int canUpgrade = 0;
        curLv = GameManager.Instance.curSaveData.myShipLv + 1;

        if (curLv >= 5) return;

        if (GameManager.Instance.curSaveData.myShipMaterials[0] == upgradeInfo.materialAmount[curLv - 1, 0])
            canUpgrade++;
        if (GameManager.Instance.curSaveData.myShipMaterials[1] == upgradeInfo.materialAmount[curLv - 1, 1])
            canUpgrade++;
        if (GameManager.Instance.curSaveData.myShipMaterials[2] == upgradeInfo.materialAmount[curLv - 1, 2])
            canUpgrade++;
        if (GameManager.Instance.curSaveData.myShipMaterials[3] == upgradeInfo.materialAmount[curLv - 1, 3])
            canUpgrade++;

        if (canUpgrade == 4)
        {
            GameManager.Instance.curSaveData.myShipLv++;
            curLv = GameManager.Instance.curSaveData.myShipLv + 1;
            GameManager.Instance.curSaveData.myShipMaterials[0] = 0;
            GameManager.Instance.curSaveData.myShipMaterials[1] = 0;
            GameManager.Instance.curSaveData.myShipMaterials[2] = 0;
            GameManager.Instance.curSaveData.myShipMaterials[3] = 0;
            if (shipLvTmp != null) shipLvTmp.text = "Lv " + (GameManager.Instance.curSaveData.myShipLv + 1);
            SetPanel();
        }
        else return;
    }

    public void OpenClick()
    {
        shipUpgradePanel.SetActive(true);
        barrier.SetActive(true);
    }
    public void CloseClick()
    {
        barrier.SetActive(false);
        shipUpgradePanel.SetActive(false);
    }


    //PutPanel Buttons//////////////////////////////////////////////////////////////////////////////////////////
    public void PlusOneClick()
    {
        if (curMaAmount >= requireMaAmount) return;

        if ((curMaAmount + 1) <= myMaAmount)
        {
            curMaAmount += 1;
            amountTxt.text = curMaAmount.ToString();
        }
    }
    public void PlusTenClick()
    {
        if (((curMaAmount + 10) <= requireMaAmount) && ((curMaAmount + 10) <= myMaAmount))
        {
            curMaAmount += 10;
            amountTxt.text = curMaAmount.ToString();
        }
        else
        {
            if ((requireMaAmount > curMaAmount) && (myMaAmount > curMaAmount))
            {
                AddAllClick();
            }
        }
    }
    public void MinusOneClick()
    {
        if (curMaAmount >= 1)
        {
            curMaAmount -= 1;
            amountTxt.text = curMaAmount.ToString();
        }
    }
    public void MinusTenClick()
    {
        if (curMaAmount > 10)
        {
            curMaAmount -= 10;
            amountTxt.text = curMaAmount.ToString();
        }
        else if (curMaAmount > 0)
        {
            curMaAmount = 0;
            amountTxt.text = curMaAmount.ToString();
        }
    }

    public void AddAllClick()
    {
        if (myMaAmount > requireMaAmount)
        {
            curMaAmount = requireMaAmount;
            amountTxt.text = curMaAmount.ToString();
        }
        else
        {
            curMaAmount = myMaAmount;
            amountTxt.text = curMaAmount.ToString();
        }
    }
    public void PutClick()
    {
        if (curMaAmount <= 0) putPanel.SetActive(false);

        if (curMaAmount <= myMaAmount)
        {
            if(im.DeleteItem(curItemCode, curMaAmount))
                GameManager.Instance.curSaveData.myShipMaterials[curMaterialIndex] += curMaAmount;
            SetPanel();
            putPanel.SetActive(false);
        }
    }
    public void CancelClick()
    {
        putPanel.SetActive(false);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

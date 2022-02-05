using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    UpgradeInfo upgradeInfo;
    public InventoryManager invenManager;

    public GameObject upgradePanel;
    public GameObject barrier;
    public Text upgradeName;
    public Text description;
    public Text currentLvl;
    public Text nextLvl;
    public Text currentInfo;
    public Text nextInfo;
    public Text priceTxt;
    public Text moneyTxt;
    public Image moneyChangeImg;
    public Text moneyChangeText;
    public Image upgradeIcon;
    public Sprite[] upgradeSprites = new Sprite[5];
    public TextMeshProUGUI digLvTmp;
    public TextMeshProUGUI boosterLvTmp;
    public TextMeshProUGUI hpLvTmp;
    public TextMeshProUGUI invenLvTmp;
    public TextMeshProUGUI resistLvTmp;
    int moneyChangeCount = 0;

    int currentUpgradeIndex;
    int price;
    bool isMaxLv = true;


    // Start is called before the first frame update
    void Start()
    {
        upgradeInfo = GameManager.Instance.upgradeInfo;
        SetUpgradeLvTxt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUpgradeLvTxt()
    {
        digLvTmp.text = "Lv " + GameManager.Instance.curSaveData.myUpgradeLvs[0];
        boosterLvTmp.text = "Lv " + GameManager.Instance.curSaveData.myUpgradeLvs[1];
        hpLvTmp.text = "Lv " + GameManager.Instance.curSaveData.myUpgradeLvs[2];
        invenLvTmp.text = "Lv " + GameManager.Instance.curSaveData.myUpgradeLvs[3];
        resistLvTmp.text = "Lv " + GameManager.Instance.curSaveData.myUpgradeLvs[4];
    }

    public void DigUpgClick()
    {
        currentUpgradeIndex = 0;
        int curLv = GameManager.Instance.curSaveData.myUpgradeLvs[0];

        upgradeIcon.sprite = upgradeSprites[0];
        upgradeName.text = "ä����";
        description.text = "ä������ ��ȭ�մϴ�.";
        currentLvl.text = "���緹��: " + curLv.ToString();
        currentInfo.text = "ä���� " + upgradeInfo.digPowerList[curLv - 1].ToString();

        if ((curLv < upgradeInfo.maxDigLvl) && (curLv >= 1))
        {
            isMaxLv = false;
            price = upgradeInfo.digPriceList[curLv - 1];
            priceTxt.text = string.Format("{0:#,0}", upgradeInfo.digPriceList[curLv - 1]);
            nextLvl.text = "��������: " + (curLv + 1).ToString();
            nextInfo.text = "ä����: " + upgradeInfo.digPowerList[curLv].ToString();
        }
        else
        {
            isMaxLv = true;
            price = 0;
            priceTxt.text = "-";
            nextLvl.text = "�ִ� �����Դϴ�.";
            nextInfo.text = "";
        }

        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        upgradePanel.SetActive(true);
        barrier.SetActive(true);
    }
    public void BoosterUpgClick()
    {
        currentUpgradeIndex = 1;

        int curLv = GameManager.Instance.curSaveData.myUpgradeLvs[currentUpgradeIndex];

        upgradeIcon.sprite = upgradeSprites[1];
        upgradeName.text = "�ν���";
        description.text = "�ν��͸� ��ȭ�մϴ�.";
        currentLvl.text = "���緹��: " + curLv.ToString();
        currentInfo.text = "�ν��� ���: " + upgradeInfo.boosterPowerList[curLv - 1].ToString()
            + System.Environment.NewLine + "�ִ� ����ӵ�: " + upgradeInfo.maxFlyList[curLv - 1].ToString();

        if ((curLv < upgradeInfo.maxBoosterLvl) && (curLv >= 1))
        {
            isMaxLv = false;
            price = upgradeInfo.boosterPriceList[curLv - 1];
            priceTxt.text = string.Format("{0:#,0}", price);
            nextLvl.text = "��������: " + (curLv + 1).ToString();
            nextInfo.text = "�ν��� ���: " + upgradeInfo.boosterPowerList[curLv].ToString()
                + System.Environment.NewLine + "�ִ� ����ӵ�: " + upgradeInfo.maxFlyList[curLv].ToString();
        }
        else
        {
            isMaxLv = true;
            price = 0;
            priceTxt.text = "-";
            nextLvl.text = "�ִ� �����Դϴ�.";
            nextInfo.text = "";
        }

        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        upgradePanel.SetActive(true);
        barrier.SetActive(true);
    }
    public void HpUpgClick()
    {
        currentUpgradeIndex = 2;

        int curLv = GameManager.Instance.curSaveData.myUpgradeLvs[currentUpgradeIndex];

        upgradeIcon.sprite = upgradeSprites[2];
        upgradeName.text = "ü��";
        description.text = "ü���� ��ȭ�մϴ�.";
        currentLvl.text = "���緹��: " + curLv.ToString();
        currentInfo.text = "ü��: " + upgradeInfo.hpAmountList[curLv - 1].ToString();

        if ((curLv < upgradeInfo.maxHpLvl) && (curLv >= 1))
        {
            isMaxLv = false;
            price = upgradeInfo.hpPriceList[curLv - 1];
            priceTxt.text = string.Format("{0:#,0}", price);
            nextLvl.text = "��������: " + (curLv + 1).ToString();
            nextInfo.text = "ü��: " + upgradeInfo.hpAmountList[curLv].ToString();
        }
        else
        {
            isMaxLv = true;
            price = 0;
            priceTxt.text = "-";
            nextLvl.text = "�ִ� �����Դϴ�.";
            nextInfo.text = "";
        }

        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        upgradePanel.SetActive(true);
        barrier.SetActive(true);
    }
    public void InvenUpgClick()
    {
        currentUpgradeIndex = 3;

        int curLv = GameManager.Instance.curSaveData.myUpgradeLvs[currentUpgradeIndex];

        upgradeIcon.sprite = upgradeSprites[3];
        upgradeName.text = "�κ��丮";
        description.text = "�κ��丮�� Ȯ���մϴ�.";
        currentLvl.text = "���緹��: " + curLv.ToString();
        currentInfo.text = "�κ��丮 �뷮: " + upgradeInfo.invenAmountList[curLv - 1].ToString();

        if ((curLv < upgradeInfo.maxInvenLvl) && (curLv >= 1))
        {
            isMaxLv = false;
            price = upgradeInfo.invenPriceList[curLv - 1];
            priceTxt.text = string.Format("{0:#,0}", price);
            nextLvl.text = "��������: " + (curLv + 1).ToString();
            nextInfo.text = "�κ��丮 �뷮: " + upgradeInfo.invenAmountList[curLv].ToString();
        }
        else
        {
            isMaxLv = true;
            price = 0;
            priceTxt.text = "-";
            nextLvl.text = "�ִ� �����Դϴ�.";
            nextInfo.text = "";
        }

        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        upgradePanel.SetActive(true);
        barrier.SetActive(true);
    }
    public void ResistUpgClick()
    {
        currentUpgradeIndex = 4;

        int curLv = GameManager.Instance.curSaveData.myUpgradeLvs[currentUpgradeIndex];

        upgradeIcon.sprite = upgradeSprites[4];
        upgradeName.text = "��缱 ����";
        description.text = "��缱 ���� �ɷ��� ��ȭ�մϴ�.";
        currentLvl.text = "���緹��: " + curLv.ToString();
        currentInfo.text = "��缱 ����: " + upgradeInfo.resistAmountList[curLv - 1].ToString();

        if ((curLv < upgradeInfo.maxResistLvl) && (curLv >= 1))
        {
            isMaxLv = false;
            price = upgradeInfo.resistPriceList[curLv - 1];
            priceTxt.text = string.Format("{0:#,0}", price);
            nextLvl.text = "��������: " + (curLv + 1).ToString();
            nextInfo.text = "��缱 ����: " + upgradeInfo.resistAmountList[curLv].ToString();
        }
        else
        {
            isMaxLv = true;
            price = 0;
            priceTxt.text = "-";
            nextLvl.text = "�ִ� �����Դϴ�.";
            nextInfo.text = "";
        }

        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        upgradePanel.SetActive(true);
        barrier.SetActive(true);
    }

    public void UpgradeClick()
    {
        if (!isMaxLv && (price <= GameManager.Instance.curSaveData.myMoney))
        {
            GameManager.Instance.curSaveData.myMoney -= price;
            GameManager.Instance.curSaveData.myUpgradeLvs[currentUpgradeIndex]++;
            moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
            StartCoroutine(ShowMoneyChangeInformation(false, price));

            SetUpgradeLvTxt();
            switch (currentUpgradeIndex)
            {
                case 0:
                    DigUpgClick();
                    break;
                case 1:
                    BoosterUpgClick();
                    break;
                case 2:
                    HpUpgClick();
                    break;
                case 3:
                    invenManager.AddSlots(4);
                    InvenUpgClick();
                    break;
                case 4:
                    ResistUpgClick();
                    break;
            }
        }
    }
    public void CloseClick()
    {
        barrier.SetActive(false);
        upgradePanel.SetActive(false);
    }

    IEnumerator ShowMoneyChangeInformation(bool isPlus, int amount)
    {
        string sign;
        if (isPlus) sign = "+";
        else sign = "-";

        moneyChangeCount++;
        moneyChangeImg.gameObject.SetActive(true);
        moneyChangeText.text = sign + string.Format("{0:#,0}", amount);
        moneyChangeImg.color = new Color(moneyChangeImg.color.r, moneyChangeImg.color.g, moneyChangeImg.color.b, 0);
        moneyChangeText.color = new Color(moneyChangeText.color.r, moneyChangeText.color.g, moneyChangeText.color.b, 0);

        moneyChangeImg.rectTransform.position = new Vector3(moneyChangeImg.rectTransform.position.x, moneyTxt.rectTransform.position.y, moneyChangeImg.rectTransform.position.z);
        while (true)
        {
            moneyChangeImg.rectTransform.position = new Vector3(moneyChangeImg.rectTransform.position.x, moneyChangeImg.rectTransform.position.y + (300 * Time.deltaTime), moneyChangeImg.rectTransform.position.z);
            moneyChangeImg.color = new Color(moneyChangeImg.color.r, moneyChangeImg.color.g, moneyChangeImg.color.b, moneyChangeImg.color.a + Time.deltaTime * 2.0f);
            moneyChangeText.color = new Color(moneyChangeText.color.r, moneyChangeText.color.g, moneyChangeText.color.b, moneyChangeText.color.a + Time.deltaTime * 2.0f);

            if (moneyChangeImg.rectTransform.position.y >= moneyTxt.rectTransform.position.y + 103)
                break;
            yield return null;
        }

        float changeRate = 0.01f;
        while (true)
        {
            moneyChangeImg.color = new Color(moneyChangeImg.color.r, moneyChangeImg.color.g, moneyChangeImg.color.b, moneyChangeImg.color.a - changeRate * Time.deltaTime);
            moneyChangeText.color = new Color(moneyChangeText.color.r, moneyChangeText.color.g, moneyChangeText.color.b, moneyChangeText.color.a - changeRate * Time.deltaTime);
            changeRate *= 1.15f;


            if (moneyChangeImg.color.a <= 0.01f)
                moneyChangeImg.color = new Color(moneyChangeImg.color.r, moneyChangeImg.color.g, moneyChangeImg.color.b, 0);
            if (moneyChangeText.color.a <= 0.01f)
            {
                moneyChangeText.color = new Color(moneyChangeText.color.r, moneyChangeText.color.g, moneyChangeText.color.b, 0);
                break;
            }
            yield return null;
        }

        moneyChangeCount--;
        if (moneyChangeCount == 0)
            moneyChangeImg.gameObject.SetActive(false);
    }
}

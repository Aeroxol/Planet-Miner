using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public InventoryManager invenManager;
    public GameObject shopCanvas;
    public GameObject buyTab;
    public GameObject sellTab;
    public GameObject sellTabContent;
    public BuyPanel buyPanel;
    public SellPanel sellPanel;
    public Text moneyTxt;
    public GameObject sellSlotPrefab;
    public List<OreData> shopItemData;
    public List<BuySlot> buySlots;
    List<SellSlot> sellSlots = new List<SellSlot>();
    public Image moneyChangeImg;
    public Text moneyChangeText;
    int moneyChangeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buySlots.Count; i++)
        {
            buySlots[i].thumbnail.sprite = shopItemData[i].artwork;
        }
        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        //CreateSellSlots();
        Invoke("CreateSellSlots", 0.5f);
        buyTab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuySlotClicked(int index)
    {
        buyPanel.InitializeBuyPanel(index);
        buyPanel.gameObject.SetActive(true);
    }
    public void ChangeMoney(int price)
    {
        GameManager.Instance.curSaveData.myMoney += price;
        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
    }
    void CreateSellSlots()
    {
        shopCanvas.SetActive(true);
        buyTab.SetActive(true);
        for (int i = 0; i < invenManager.itemData.Count; i++)
        {
            GameObject temp = Instantiate(sellSlotPrefab);
            temp.transform.SetParent(sellTabContent.transform);
            sellSlots.Add(temp.GetComponent<SellSlot>());
            sellSlots[i].invenManager = invenManager;
            sellSlots[i].shopManager = GetComponent<ShopManager>();
            sellSlots[i].Initialize(i);
        }
        RefreshSellSlots();
        //buyTab.SetActive(false);
        shopCanvas.SetActive(false);
    }
    public void RefreshSellSlots()
    {
        for (int i = 0; i < sellSlots.Count; i++)
        {
            if (invenManager.itemTotal[i] > 0)
            {
                sellSlots[i].amount = invenManager.itemTotal[i];
                sellSlots[i].amountTmp.text = sellSlots[i].amount.ToString();
                sellSlots[i].gameObject.SetActive(true);
            }
            else
            {
                sellSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenShopClick()
    {
        shopCanvas.SetActive(true);
    }
    public void CloseShopClick()
    {
        shopCanvas.SetActive(false);
        buyPanel.gameObject.SetActive(false);
        sellPanel.gameObject.SetActive(false);
    }
    public void BuyTabBtnClick()
    {
        sellTab.SetActive(false);
        sellPanel.gameObject.SetActive(false);
        buyTab.SetActive(true);
    }
    public void SellTabBtnClick()
    {
        buyTab.SetActive(false);
        buyPanel.gameObject.SetActive(false);
        sellTab.SetActive(true);
    }

    public IEnumerator ShowMoneyChangeInformation(bool isPlus, int amount)
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

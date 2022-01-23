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

    // Start is called before the first frame update
    void Start()    
    {
        for(int i=0; i<buySlots.Count; i++)
        {
            buySlots[i].thumbnail.sprite = shopItemData[i].artwork;
        }
        moneyTxt.text = GameManager.Instance.myMoney.ToString();
        CreateSellSlots();
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
        GameManager.Instance.myMoney += price;
        moneyTxt.text = GameManager.Instance.myMoney.ToString();
    }
    void CreateSellSlots()
    {
        shopCanvas.SetActive(true);
        buyTab.SetActive(true);
        for(int i=0; i<invenManager.itemData.Count; i++)
        {
            GameObject temp = Instantiate(sellSlotPrefab);
            temp.transform.SetParent(sellTabContent.transform);
            sellSlots.Add(temp.GetComponent<SellSlot>());
            sellSlots[i].invenManager = invenManager;
            sellSlots[i].shopManager = GetComponent<ShopManager>();
            sellSlots[i].Initialize(i);
        }
        RefreshSellSlots();
        buyTab.SetActive(false);
        shopCanvas.SetActive(false);
    }
    public void RefreshSellSlots()
    {
        for(int i=0; i < sellSlots.Count; i++)
        {
            if (invenManager.itemTotal[i] > 0)
            {
                sellSlots[i].amount = invenManager.itemTotal[i];
                sellSlots[i].amountTxt.text = sellSlots[i].amount.ToString();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellSlot : MonoBehaviour
{
    int index;
    int sellingPrice;
    [HideInInspector] public int amount;
    [HideInInspector] public InventoryManager invenManager;
    [HideInInspector] public ShopManager shopManager;

    public Image thumbnail;
    public TextMeshProUGUI amountTmp;
    public Text nameTxt;
    public Text priceTxt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(int index)
    {
        this.index = index;
        sellingPrice = invenManager.itemData[index].price;
        if (invenManager.itemData[index].isUsable) sellingPrice = (int)(sellingPrice * 0.7);
        if (invenManager.itemTotal.Length > index) amount = invenManager.itemTotal[index];

        thumbnail.sprite = invenManager.itemData[index].artwork;
        nameTxt.text = invenManager.itemData[index].itemName;
        priceTxt.text = string.Format("{0:#,0}", sellingPrice);
        amountTmp.text = amount.ToString();
    }

    public void SellAllClick()
    {
        invenManager.DeleteItem(index, amount);
        shopManager.ChangeMoney(sellingPrice * amount);
        shopManager.RefreshSellSlots();
        shopManager.StartCoroutine(shopManager.ShowMoneyChangeInformation(true, sellingPrice * amount));
    }
    public void SellClick()
    {
        shopManager.sellPanel.InitializeSellPanel(index);
        shopManager.sellPanel.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellPanel : MonoBehaviour
{
    public InventoryManager invenManager;
    public ShopManager shopManager;
    public Text itemNameTxt;
    public Image thumbnail;
    //public Text thumbnailAmountTxt;
    public Text amountTxt;
    public TextMeshProUGUI thumbnailAmountTmp;
    public Button plusOneBtn;
    public Button plusTenBtn;
    public Button minusOneBtn;
    public Button minusTenBtn;
    public Button sellBtn;
    public Button cancelBtn;
    public Text priceTxt;

    int itemCode;
    int sellingPrice;
    int totalPrice;
    int maxAmount;
    int amount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeSellPanel(int itemCode)
    {
        this.itemCode = itemCode;
        thumbnail.sprite = invenManager.itemData[itemCode].artwork;
        maxAmount = invenManager.itemTotal[itemCode];
        thumbnailAmountTmp.text = maxAmount.ToString();
        itemNameTxt.text = invenManager.itemData[itemCode].itemName;
        sellingPrice = invenManager.itemData[itemCode].price;
        if (invenManager.itemData[itemCode].isUsable) sellingPrice = (int)(sellingPrice * 0.7);
        totalPrice = sellingPrice;
        priceTxt.text = string.Format("{0:#,0}", sellingPrice);
        amount = 1;
        amountTxt.text = "1";
    }

    public void PlusOneClick()
    {
        if (amount < maxAmount)
        {
            amount++;
            totalPrice += sellingPrice;
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
    }
    public void PlusTenClick()
    {
        if ((amount + 10) < maxAmount)
        {
            amount += 10;
            totalPrice += (sellingPrice * 10);
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
        else
        {
            amount = maxAmount;
            totalPrice = sellingPrice * amount;
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
    }
    public void MinusOneClick()
    {
        if (amount > 1)
        {
            amount--;
            totalPrice -= sellingPrice;
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
    }
    public void MinusTenClick()
    {
        if (amount > 10)
        {
            amount -= 10;
            totalPrice -= (sellingPrice * 10);
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
        else if (amount > 1)
        {
            amount = 1;
            totalPrice = sellingPrice;
            amountTxt.text = amount.ToString();
            priceTxt.text = string.Format("{0:#,0}", totalPrice);
        }
    }

    public void SellClick()
    {
        invenManager.DeleteItem(itemCode, amount);
        shopManager.ChangeMoney(totalPrice);
        shopManager.StartCoroutine(shopManager.ShowMoneyChangeInformation(true, totalPrice));
        gameObject.SetActive(false);
    }
    public void CancelClick()
    {
        gameObject.SetActive(false);
    }
}

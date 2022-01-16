using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    public InventoryManager invenManager;
    public ShopManager shopManager;
    public Text itemNameTxt;
    public Image thumbnail;
    public Text thumbnailAmountTxt;
    public Text amountTxt;
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
        thumbnailAmountTxt.text = maxAmount.ToString();
        itemNameTxt.text = invenManager.itemData[itemCode].itemName;
        sellingPrice = invenManager.itemData[itemCode].price;
        if(invenManager.itemData[itemCode].isUsable) sellingPrice = (int)(sellingPrice * 0.7);
        totalPrice = sellingPrice;
        priceTxt.text = sellingPrice.ToString();
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
            priceTxt.text = totalPrice.ToString();
        }
    }
    public void PlusTenClick()
    {
        if ((amount + 10) < maxAmount)
        {
            amount += 10;
            totalPrice += (sellingPrice * 10);
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
        else
        {
            amount = maxAmount;
            totalPrice = sellingPrice * amount;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }
    public void MinusOneClick()
    {
        if (amount > 1)
        {
            amount--;
            totalPrice -= sellingPrice;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }
    public void MinusTenClick()
    {
        if (amount > 10)
        {
            amount-=10;
            totalPrice -= (sellingPrice * 10);
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
        else if (amount > 1)
        {
            amount = 1;
            totalPrice = sellingPrice;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }

    public void SellClick()
    {
        invenManager.DeleteItem(itemCode, amount);
        shopManager.ChangeMoney(totalPrice);
        gameObject.SetActive(false);
    }
    public void CancelClick()
    {
        gameObject.SetActive(false);
    }
}

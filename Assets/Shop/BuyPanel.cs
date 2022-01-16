using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ShopManager shopManager;
    public Text itemNameTxt;
    public Text descriptionTxt;
    public Image thumbnail;
    public Text amountTxt;
    public Button plusOneBtn;
    public Button plusTenBtn;
    public Button minusOneBtn;
    public Button minusTenBtn;
    public Button buyBtn;
    public Button cancelBtn;
    public Text priceTxt;

    int index;
    int itemCode;
    int amount;
    int maxAvailable;
    int price;
    int totalPrice;

    [HideInInspector] public List<ItemInSlot> items;

    // Start is called before the first frame update
    void Start()
    {
        items = GameManager.Instance.myItems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBuyPanel(int index)
    {
        this.index = index;
        itemCode = shopManager.shopItemData[index].itemCode;
        thumbnail.sprite = shopManager.shopItemData[index].artwork;
        itemNameTxt.text = shopManager.shopItemData[index].itemName;
        descriptionTxt.text = shopManager.shopItemData[index].description;
        price = shopManager.shopItemData[index].price;
        totalPrice = price;
        priceTxt.text = price.ToString();
        amount = 1;
        amountTxt.text = "1";
        maxAvailable = CheckMaxAvailable();
    }

    public void PlusOneClick()
    {
        if (((totalPrice + price) <= shopManager.test_money) && ((amount + 1) <= maxAvailable))
        {
            amount++;
            totalPrice += price;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }
    public void PlusTenClick()
    {
        int available = 10;

        if (maxAvailable - amount < 10) available = (maxAvailable - amount);
        if (available < 1) return;

        if ((totalPrice + (price * available)) <= shopManager.test_money)
        {
            amount += available;
            totalPrice += (price * available);
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
        else if ((shopManager.test_money / price) > amount)
        {
            amount = shopManager.test_money / price;
            totalPrice = amount * price;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }

    }
    public void MinusOneClick()
    {
        if (amount > 1)
        {
            amount--;
            totalPrice -= price;
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }
    public void MinusTenClick()
    {
        if (amount > 1)
        {
            if (amount > 10)
            {
                amount -= 10;
                totalPrice -= (price * 10);
            }
            else if (amount > 1)
            {
                amount = 1;
                totalPrice = price;
            }
            amountTxt.text = amount.ToString();
            priceTxt.text = totalPrice.ToString();
        }
    }

    int CheckMaxAvailable()
    {
        int availableAmount;

        availableAmount = (inventoryManager.maxSlot - items.Count) * shopManager.shopItemData[index].maxAmount;

        for(int i=0; i<items.Count; i++)
        {
            if (items[i].itemCode == itemCode)
            {
                availableAmount += shopManager.shopItemData[index].maxAmount - items[i].amount;
            }
        }

        return availableAmount;
    }

    public void BuyClick()
    {
        if (totalPrice > shopManager.test_money) return;

        shopManager.ChangeMoney(-totalPrice);
        inventoryManager.AddItem(itemCode, amount);
        gameObject.SetActive(false);
    }
    public void CancelClick()
    {
        gameObject.SetActive(false);
    }
}

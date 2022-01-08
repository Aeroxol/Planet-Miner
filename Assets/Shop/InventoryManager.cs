using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInSlot
{
    public int itemCode;
    public int amount;

    public ItemInSlot(int itemCode, int amount)
    {
        this.itemCode = itemCode;
        this.amount = amount;
    }
}

public class InventoryManager : MonoBehaviour
{
    public List<OreData> itemData;
    public List<ItemInSlot> items;// = new List<ItemInSlot>();
    public Canvas inventoryCanvas;
    public GameObject content;
    public GameObject slotPrefab;
    public Button useBtn;
    public Button registerBtn;
    public ItemEffectManager itemEffectManager;

    public Image infoBox;
    public Text itemName;
    public Text itemDescription;
    public Image hotkeyImg;

    //List<GameObject> slots = new List<GameObject>();
    [HideInInspector] public List<InventorySlot> slots = new List<InventorySlot>();
    [HideInInspector] public int clickedSlotIndex = -1;
    [HideInInspector] public float middleSlotPos = 0;
    [HideInInspector] public int registeredItemCode = -1;
    int[] itemTotal;

     // Start is called before the first frame update
    void Start()
    {
        items = GameManager.Instance.myItems;
        inventoryCanvas.gameObject.SetActive(true);
        for (int i=0; i<20; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.transform.SetParent(content.transform);
            slots.Add(temp.GetComponent<InventorySlot>());
        }
        inventoryCanvas.gameObject.SetActive(false);

        itemTotal = new int[itemData.Count];

        items.Add(new ItemInSlot(5, 2));//test
        items.Add(new ItemInSlot(6, 2));//test
        items.Add(new ItemInSlot(7, 2));//test
        RefreshSlots();//test
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool addItem(int itemCode, int amount)
    {
        bool canGatItem = true;

        if (items.Count == 0) items.Add(new ItemInSlot(itemCode, amount));
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemCode == itemCode)
                {
                    if (items[i].amount < itemData[itemCode].maxAmount)
                    {
                        items[i].amount += amount;
                        if (items[i].amount > itemData[itemCode].maxAmount)
                        {
                            amount = items[i].amount - itemData[itemCode].maxAmount;
                            items[i].amount = itemData[itemCode].maxAmount;

                        }
                        else break;
                    }
                }
                if (i + 1 == items.Count)
                {
                    items.Add(new ItemInSlot(itemCode, amount));
                    break;
                }
            }
        }

        if (items.Count > slots.Count)
        {
            items.RemoveAt(items.Count - 1);
            canGatItem = false;
        }
        else RefreshSlots();
        
        return canGatItem;
    }

    void RefreshSlots()
    {
        for (int i = 0; i < itemTotal.Length; i++) itemTotal[i] = 0;

        for (int i=0; i<slots.Count; i++)
        {
            if (i<items.Count)
            {
                slots[i].thumbnail.sprite = itemData[items[i].itemCode].artwork;
                slots[i].amount.text = items[i].amount.ToString();
                slots[i].thumbnail.gameObject.SetActive(true);
                slots[i].amount.gameObject.SetActive(true);

                itemTotal[items[i].itemCode] += items[i].amount;
            }
            else
            {
                slots[i].thumbnail.gameObject.SetActive(false);
                slots[i].amount.gameObject.SetActive(false);
            }
        }
    }

    public void OpenClick()
    {
        inventoryCanvas.gameObject.SetActive(true);
    }
    public void CloseClick()
    {
        infoBox.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
        registerBtn.gameObject.SetActive(false);
        slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        clickedSlotIndex = -1;
        inventoryCanvas.gameObject.SetActive(false);
    }

    public void UseClick()
    {
        itemEffectManager.ItemEffect(items[clickedSlotIndex].itemCode);
        items[clickedSlotIndex].amount -= 1;
        itemTotal[items[clickedSlotIndex].itemCode] -= 1;
        if (itemTotal[items[clickedSlotIndex].itemCode] <= 0)
        {
            registeredItemCode = -1;
            hotkeyImg.gameObject.SetActive(false);
        }
        if (items[clickedSlotIndex].amount <= 0)
        {
            items.RemoveAt(clickedSlotIndex);
            useBtn.gameObject.SetActive(false);
            infoBox.gameObject.SetActive(false);
            slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        }
        RefreshSlots();
    }

    public void RegisterClick()
    {
        registeredItemCode = items[clickedSlotIndex].itemCode;
        hotkeyImg.sprite = itemData[registeredItemCode].artwork;
        hotkeyImg.gameObject.SetActive(true);
    }

    public void HotkeyClick()
    {
        if (registeredItemCode == -1) return;
        if (items.Count == 0) return;
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemCode == registeredItemCode)
                {
                    itemEffectManager.ItemEffect(registeredItemCode);
                    items[i].amount -= 1;
                    itemTotal[registeredItemCode] -= 1;
                    if (items[i].amount <= 0)
                    {
                        items.RemoveAt(i);
                    }
                    if (itemTotal[registeredItemCode] <= 0)
                    {
                        registeredItemCode = -1;
                        hotkeyImg.gameObject.SetActive(false);
                    }
                    RefreshSlots();
                    break;
                }
            }
        }
    }
}

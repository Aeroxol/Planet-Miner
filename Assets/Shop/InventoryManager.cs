using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInSlot
{
    public int itemCode;
    public int maxAmount = 5;
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
    public List<ItemInSlot> items = new List<ItemInSlot>();
    public Canvas inventoryCanvas;
    public GameObject content;
    public GameObject slotPrefab;
    public Button useBtn;
    public Button hotKeyBtn;
    public ItemEffectManager itemEffectManager;

    public Image infoBox;
    public Text itemName;
    public Text itemDescription;

    //List<GameObject> slots = new List<GameObject>();
    [HideInInspector] public List<InventorySlot> slots = new List<InventorySlot>();
    [HideInInspector] public int clickedSlotIndex = -1;
    [HideInInspector] public float middleSlotPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.gameObject.SetActive(true);
        for (int i=0; i<20; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.transform.SetParent(content.transform);
            slots.Add(temp.GetComponent<InventorySlot>());
        }
        inventoryCanvas.gameObject.SetActive(false);

        items.Add(new ItemInSlot(5, 2));//test
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
                    if (items[i].amount < items[i].maxAmount)
                    {
                        items[i].amount += amount;
                        if (items[i].amount > items[i].maxAmount)
                        {
                            amount = items[i].amount - items[i].maxAmount;
                            items[i].amount = items[i].maxAmount;

                        }
                        else break;
                    }
                }
                if (i + 1 == items.Count)
                {
                    items.Add(new ItemInSlot(itemCode, amount));
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
        for(int i=0; i<slots.Count; i++)
        {
            if (i<items.Count)
            {
                slots[i].thumbnail.sprite = itemData[items[i].itemCode].artwork;
                slots[i].amount.text = items[i].amount.ToString();
                slots[i].thumbnail.gameObject.SetActive(true);
                slots[i].amount.gameObject.SetActive(true);
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
        inventoryCanvas.gameObject.SetActive(false);
    }

    public void UseClick()
    {
        itemEffectManager.ItemEffect(items[clickedSlotIndex].itemCode);
        items[clickedSlotIndex].amount -= 1;
        if (items[clickedSlotIndex].amount <= 0)
        {
            items.RemoveAt(clickedSlotIndex);
            useBtn.gameObject.SetActive(false);
            infoBox.gameObject.SetActive(false);
            slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        }
        RefreshSlots();
    }
}

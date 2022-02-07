using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public bool isLobby; //Lobby인지 Stage 인지
    public List<OreData> itemData;
    public List<ItemInSlot> items;// = new List<ItemInSlot>();
    public Canvas inventoryCanvas;
    public GameObject content;
    public GameObject slotPrefab;
    public Text moneyTxt;
    public Button useBtn;
    public Button registerBtn;
    public ItemEffectManager itemEffectManager;
    public ShopManager shopManager;

    public Image infoBox;
    public Text itemName;
    public Text itemDescription;
    public ContentSizeFitter infoBoxCsf;
    public Image hotkeyImg;

    public PlayerManager playerManager;

    //List<GameObject> slots = new List<GameObject>();
    [HideInInspector] public List<InventorySlot> slots = new List<InventorySlot>();
    [HideInInspector] public int clickedSlotIndex = -1;
    [HideInInspector] public float middleSlotPos = 0;
    //[HideInInspector] public int registeredItemCode = -1;
    [HideInInspector] public int maxSlot = 20;
    [HideInInspector] public int[] itemTotal;

    public TextMeshProUGUI hkAmountTmp;
    public GridLayoutGroup gridLayoutGroup;

     // Start is called before the first frame update
    void Start()
    {
        items = GameManager.Instance.curSaveData.myItems;
        maxSlot = GameManager.Instance.upgradeInfo.invenAmountList[GameManager.Instance.curSaveData.myUpgradeLvs[3] - 1];

        if (Screen.width >= 1440)
        {
            gridLayoutGroup.cellSize = new Vector2(250, 250);
            gridLayoutGroup.padding = new RectOffset(0,0,0,0);
            gridLayoutGroup.spacing = new Vector2(-15, -15);
        }


        inventoryCanvas.gameObject.SetActive(true);
        for (int i=0; i<maxSlot; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.transform.SetParent(content.transform);
            slots.Add(temp.GetComponent<InventorySlot>());
        }
        inventoryCanvas.gameObject.SetActive(false);

        itemTotal = new int[itemData.Count];

        //items.Add(new ItemInSlot(5, 2));//test
        //items.Add(new ItemInSlot(6, 2));//test
        //items.Add(new ItemInSlot(7, 2));//test
        RefreshSlots();

        if (hotkeyImg != null)
        {
            if ((GameManager.Instance.curSaveData.registeredItemCode != -1)
                && itemTotal[GameManager.Instance.curSaveData.registeredItemCode] > 0)
            {
                hotkeyImg.sprite = itemData[GameManager.Instance.curSaveData.registeredItemCode].artwork;
                hkAmountTmp.text = itemTotal[GameManager.Instance.curSaveData.registeredItemCode].ToString();
                hotkeyImg.gameObject.SetActive(true);
                hkAmountTmp.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(int itemCode, int amount)
    {
        bool canGatItem = true;
        int myMaxAmount = itemData[itemCode].maxAmount;

        if (items.Count == 0)
        {
            if (amount <= myMaxAmount)
            {
                items.Add(new ItemInSlot(itemCode, amount));
                amount = 0;
            }
            else
            {
                items.Add(new ItemInSlot(itemCode, myMaxAmount));
                amount -= myMaxAmount;
            }
        }

        if (amount > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemCode == itemCode)
                {
                    if (items[i].amount < myMaxAmount)
                    {
                        items[i].amount += amount;
                        if (items[i].amount > myMaxAmount)
                        {
                            amount = items[i].amount - myMaxAmount;
                            items[i].amount = myMaxAmount;

                        }
                        else break;
                    }
                }
                if (i + 1 == items.Count)
                {
                    if (amount <= myMaxAmount)
                    {
                        items.Add(new ItemInSlot(itemCode, amount));
                        break;
                    }
                    else
                    {
                        items.Add(new ItemInSlot(itemCode, myMaxAmount));
                        amount -= myMaxAmount;
                    }
                }
            }
        }

        if (items.Count > slots.Count)
        {
            items.RemoveAt(items.Count - 1);
            canGatItem = false;
        }
        else {
            if (!GameManager.Instance.curSaveData.gotUnoptanium && (itemCode == 13))
                GameManager.Instance.curSaveData.gotUnoptanium = true;
            RefreshSlots();
        }
        
        return canGatItem;
    }

    public bool DeleteItem(int itemCode, int amount)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemCode == itemCode)
            {
                if (items[i].amount > amount)
                {
                    items[i].amount -= amount;
                    amount = 0;
                    break;
                }
                else
                {
                    amount -= items[i].amount;
                    items.RemoveAt(i);
                    if (amount <= 0) break;
                }
            }
        }

        RefreshSlots();
        if (amount == 0) return true;
        else return false;
    }

    void RefreshSlots()
    {
        for (int i = 0; i < itemTotal.Length; i++) itemTotal[i] = 0;

        for (int i=0; i<slots.Count; i++)
        {
            if (i<items.Count)
            {
                slots[i].thumbnail.sprite = itemData[items[i].itemCode].artwork;
                slots[i].amountTmp.text = items[i].amount.ToString();
                slots[i].thumbnail.gameObject.SetActive(true);
                slots[i].amountTmp.gameObject.SetActive(true);

                itemTotal[items[i].itemCode] += items[i].amount;
            }
            else
            {
                slots[i].thumbnail.gameObject.SetActive(false);
                slots[i].amountTmp.gameObject.SetActive(false);
            }
        }

        if (hotkeyImg != null)
        {
            if (GameManager.Instance.curSaveData.registeredItemCode != -1)
            {
                if (itemTotal[GameManager.Instance.curSaveData.registeredItemCode] <= 0)
                {
                    hotkeyImg.gameObject.SetActive(false);
                    hkAmountTmp.gameObject.SetActive(false);
                }
                hkAmountTmp.text = itemTotal[GameManager.Instance.curSaveData.registeredItemCode].ToString();
            }
        }

        shopManager.RefreshSellSlots();
    }

    public void AddSlots(int amount)
    {
        if (amount < 0) return;

        //inventoryCanvas.gameObject.SetActive(true);
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(slotPrefab);
            temp.transform.SetParent(content.transform);
            slots.Add(temp.GetComponent<InventorySlot>());
        }
        //inventoryCanvas.gameObject.SetActive(false);
        RefreshSlots();
    }

    public void OpenClick()
    {
        inventoryCanvas.gameObject.SetActive(true);
        moneyTxt.text = string.Format("{0:#,0}", GameManager.Instance.curSaveData.myMoney);
        if (playerManager != null)
            playerManager.PausePlayer(true);
    }
    public void CloseClick()
    {
        if (playerManager != null)
            playerManager.PausePlayer(false);

        infoBox.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
        registerBtn.gameObject.SetActive(false);
        if ((clickedSlotIndex != -1) && (clickedSlotIndex < slots.Count)) 
            slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        clickedSlotIndex = -1;
        inventoryCanvas.gameObject.SetActive(false);
    }

    void CloseWithoutResume() // Close without resuming player move
    {
        infoBox.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
        registerBtn.gameObject.SetActive(false);
        if ((clickedSlotIndex != -1) && (clickedSlotIndex < slots.Count))
            slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        clickedSlotIndex = -1;
        inventoryCanvas.gameObject.SetActive(false);
    }

    public void UseClick()
    {
        bool success = itemEffectManager.ItemEffect(items[clickedSlotIndex].itemCode);
        if (!success) return;
        int tempItemCode = items[clickedSlotIndex].itemCode;
        items[clickedSlotIndex].amount -= 1;
        itemTotal[items[clickedSlotIndex].itemCode] -= 1;
        if (itemTotal[items[clickedSlotIndex].itemCode] <= 0)
        {
            GameManager.Instance.curSaveData.registeredItemCode = -1;
            hkAmountTmp.text = "0";
            hotkeyImg.gameObject.SetActive(false);
            hkAmountTmp.gameObject.SetActive(false);
        }
        if (items[clickedSlotIndex].amount <= 0)
        {
            items.RemoveAt(clickedSlotIndex);
            useBtn.gameObject.SetActive(false);
            registerBtn.gameObject.SetActive(false);
            infoBox.gameObject.SetActive(false);
            slots[clickedSlotIndex].outline.gameObject.SetActive(false);
        }
        RefreshSlots();

        switch (tempItemCode)
        {
            case 17: //Dynamite
                CloseClick();
                break;
            case 18: //DynamiteB
                CloseClick();
                break;
            case 19: //RocketBomb
                CloseClick();
                break;
            case 20: //Warp
                CloseWithoutResume();
                break;
        }
    }

    public void RegisterClick()
    {
        GameManager.Instance.curSaveData.registeredItemCode = items[clickedSlotIndex].itemCode;
        hkAmountTmp.text = itemTotal[GameManager.Instance.curSaveData.registeredItemCode].ToString();
        hotkeyImg.sprite = itemData[GameManager.Instance.curSaveData.registeredItemCode].artwork;
        hotkeyImg.gameObject.SetActive(true);
        hkAmountTmp.gameObject.SetActive(true);
    }

    public void HotkeyClick()
    {
        if (GameManager.Instance.curSaveData.registeredItemCode == -1) return;
        if (items.Count == 0) return;
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemCode == GameManager.Instance.curSaveData.registeredItemCode)
                {
                    bool success = itemEffectManager.ItemEffect(GameManager.Instance.curSaveData.registeredItemCode);
                    if (!success) return;

                    items[i].amount -= 1;
                    itemTotal[GameManager.Instance.curSaveData.registeredItemCode] -= 1;
                    if (items[i].amount <= 0)
                    {
                        items.RemoveAt(i);
                    }
                    if (itemTotal[GameManager.Instance.curSaveData.registeredItemCode] <= 0)
                    {
                        GameManager.Instance.curSaveData.registeredItemCode = -1;
                        hkAmountTmp.text = "0";
                        hotkeyImg.gameObject.SetActive(false);
                        hkAmountTmp.gameObject.SetActive(false);
                    }
                    RefreshSlots();
                    hkAmountTmp.text = itemTotal[GameManager.Instance.curSaveData.registeredItemCode].ToString();
                    break;
                }
            }
        }
    }

    public void moneyCheat()
    {
        GameManager.Instance.curSaveData.myMoney += 100000;
    }
}

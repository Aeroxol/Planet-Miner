using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ScrollRect parentSR;
    RectTransform rectTransform;
    int myIndex;
    bool isDragging = false;

    public Image outline;
    public Image thumbnail;
    public TextMeshProUGUI amountTmp;

    InventoryManager im;

    Vector3 infoBoxPaddingDownRight = new Vector3(80, -80);
    Vector3 infoBoxPaddingUpRight = new Vector3(80, 80);
    Vector3 infoBoxPaddingDownLeft = new Vector3(-80, -80);
    Vector3 infoBoxPaddingUpLeft = new Vector3(-80, 80);

    // Start is called before the first frame update
    void Start()
    {
        parentSR = transform.parent.parent.parent.GetComponent<ScrollRect>();
        rectTransform = GetComponent<RectTransform>();
        myIndex = transform.GetSiblingIndex();

        im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (im.clickedSlotIndex == myIndex)
        {
            //Debug.Log(rectTransform.position.y);

            if (myIndex % 4 <= 1)
            {
                if (rectTransform.position.y > Screen.height * 0.6f)//745
                {
                    im.infoBox.rectTransform.pivot = Vector2.up;
                    im.infoBox.rectTransform.position = rectTransform.position + infoBoxPaddingDownRight;
                }
                else
                {
                    im.infoBox.rectTransform.pivot = Vector2.zero;
                    im.infoBox.rectTransform.position = rectTransform.position + infoBoxPaddingUpRight;
                }
            }
            else
            {
                if (rectTransform.position.y > Screen.height * 0.6f)//745
                {
                    im.infoBox.rectTransform.pivot = Vector2.one;
                    im.infoBox.rectTransform.position = rectTransform.position + infoBoxPaddingDownLeft;
                }
                else
                {
                    im.infoBox.rectTransform.pivot = Vector2.right;
                    im.infoBox.rectTransform.position = rectTransform.position + infoBoxPaddingUpLeft;
                }
            }

            if (im.infoBox.rectTransform.anchoredPosition.y <= -360)
                im.infoBox.rectTransform.anchoredPosition = new Vector2(im.infoBox.rectTransform.anchoredPosition.x, -360);
            else if (im.infoBox.rectTransform.anchoredPosition.y >= 380)
                im.infoBox.rectTransform.anchoredPosition = new Vector2(im.infoBox.rectTransform.anchoredPosition.x, 380);
        }
    }

    public void SlotClick()
    {
        if (isDragging) return;

        if (myIndex < im.items.Count)
        {
            if (im.clickedSlotIndex == myIndex)
            {
                outline.gameObject.SetActive(false);//
                im.infoBox.gameObject.SetActive(false);//
                im.useBtn.gameObject.SetActive(false);
                im.registerBtn.gameObject.SetActive(false);
                im.clickedSlotIndex = -1;//
            }
            else
            {
                if (im.clickedSlotIndex != -1) im.slots[im.clickedSlotIndex].outline.gameObject.SetActive(false);

                outline.gameObject.SetActive(true);
                im.infoBox.gameObject.SetActive(true);
                im.itemName.text = im.itemData[im.items[myIndex].itemCode].itemName;

                if (im.itemData[im.items[myIndex].itemCode].isUsable)
                {
                    ActivateUseAndRegBtn();

                    im.itemDescription.gameObject.SetActive(true);
                    im.itemDescription.text = im.itemData[im.items[myIndex].itemCode].description;
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)im.infoBoxCsf.transform);//
                }
                else
                {
                    im.useBtn.gameObject.SetActive(false);
                    im.registerBtn.gameObject.SetActive(false);

                    im.itemDescription.gameObject.SetActive(false);
                }

                im.clickedSlotIndex = myIndex;
            }
        }
        else
        {
            if (im.clickedSlotIndex != -1) im.slots[im.clickedSlotIndex].outline.gameObject.SetActive(false);
            im.clickedSlotIndex = -1;
            im.infoBox.gameObject.SetActive(false);
            im.useBtn.gameObject.SetActive(false);
            im.registerBtn.gameObject.SetActive(false);
            im.itemName.text = "";
        }
    }

    void ActivateUseAndRegBtn()
    {
        if (im.isLobby) return;
        else
        {
            im.useBtn.gameObject.SetActive(true);
            im.registerBtn.gameObject.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        isDragging = true;
        parentSR.OnBeginDrag(e);
    }
    public void OnDrag(PointerEventData e)
    {
        parentSR.OnDrag(e);
    }
    public void OnEndDrag(PointerEventData e)
    {
        isDragging = false;
        parentSR.OnEndDrag(e);
    }
}

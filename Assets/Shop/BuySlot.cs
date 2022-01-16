using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
    public ShopManager shopManager;
    public Image thumbnail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuySlotClick()
    {
        shopManager.BuySlotClicked(transform.GetSiblingIndex());
    }
}

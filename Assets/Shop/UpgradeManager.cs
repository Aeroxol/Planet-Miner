using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Canvas UpgradeCanvas;
    public PlayerManager player;
    public Text digLvlText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OpenClick()
    {
        UpgradeCanvas.gameObject.SetActive(true);
    }
    public void CloseClick()
    {
        UpgradeCanvas.gameObject.SetActive(false);
    }
    public void DigUpgradeClick()
    {
        player.digPower++;
        digLvlText.text = "Current:" + player.digPower;
    }
}

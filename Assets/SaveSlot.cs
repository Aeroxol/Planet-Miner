using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    public Text nameText;
    //public TextMeshProUGUI nameTmp;
    public Button btnStart;
    public Button btnDelete;
    public TitleScene titleScene;

    public void Start()
    {
        btnStart.onClick.AddListener(BtnStart);
        btnDelete.onClick.AddListener(BtnDelete);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(SaveSlot obj in titleScene.saveSlotList)
        {
            obj.btnStart.gameObject.SetActive(false);
            obj.btnDelete.gameObject.SetActive(false);
        }
        btnStart.gameObject.SetActive(true);
        btnDelete.gameObject.SetActive(true);
    }

    public void BtnStart()
    {
        SaveData newSaveData = SaveData.Load(nameText.text);
        GameManager.Instance.curSaveData = newSaveData;
        if (newSaveData.curStageMap == null)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            GameManager.Instance.loadingManager.LoadScene(2);
        }
    }

    public void BtnDelete()
    {
        titleScene.deletePanel.gameObject.SetActive(true);
        titleScene.deleteText.text = nameText.text;
    }
}

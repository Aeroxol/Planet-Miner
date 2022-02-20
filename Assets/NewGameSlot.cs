using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewGameSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject newGamePanel;
    public Button confirmBtn, cancelBtn;
    public InputField inputField;

    TitleScene titleScene;

    private void Start()
    {
        titleScene = GameObject.Find("TitleScene").GetComponent<TitleScene>();
    }

    public void BtnConfirm()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        for(int i=0;i<titleScene.saveSlotList.Count; i++)
        {
            if (inputField.text == titleScene.saveSlotList[i].nameText.text)
                return;
        }

        GameManager.Instance.curSaveData = new SaveData(inputField.text);
        SaveData.Save(GameManager.Instance.curSaveData);
        SceneManager.LoadScene(1);
    }

    public void BtnCancel()
    {
        newGamePanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        newGamePanel.SetActive(true);
    }
}

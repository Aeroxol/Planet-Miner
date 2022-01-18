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

    public void BtnConfirm()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        GameManager.Instance.curSaveData = new SaveData(inputField.text);
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

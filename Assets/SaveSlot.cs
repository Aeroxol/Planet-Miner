using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    public Text nameText;
    public Button btnStart;
    public Button btnDelete;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.curSaveData = new SaveData(nameText.text);
        SceneManager.LoadScene(1);
    }
}

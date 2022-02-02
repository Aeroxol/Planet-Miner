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
        SaveData newSaveData = SaveData.Load(nameText.text);
        GameManager.Instance.curSaveData = newSaveData;
        if (newSaveData.curStageMap == null)
        {
            SceneManager.LoadScene(1);
        }else
        {
            GameManager.Instance.loadingManager.LoadScene(2);
            //AsyncOperation ao =  SceneManager.LoadSceneAsync(2);
            //ao.allowSceneActivation = false;
            //SceneManager.LoadScene(2);
        }
    }
}
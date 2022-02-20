using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveStage : MonoBehaviour
{
    //떠나기
    public GameObject askLeave;

    //게임오버
    public GameObject gameOverPanel;
    public GameObject itemLostTxt;
    public GameObject itemProtectedTxt;

 

    public void GameOver(bool itemProtected)
    {
        gameOverPanel.SetActive(true);
        if (itemProtected)
        {
            itemProtectedTxt.SetActive(true);
        }
        else
        {
            itemLostTxt.SetActive(true);
            GameManager.Instance.curSaveData.myItems.Clear();
        }

    }

    public void LeaveBtnClick()
    {
        askLeave.SetActive(true);
    }
    public void LeaveYesClick()
    {
        GameManager.Instance.curSaveData.curStageMap = null;
        SoundManager.Stop("hit");
        SoundManager.Stop("maintheme");
        SoundManager.Play("title");
        SceneManager.LoadScene("LobbyScene");
    }
    public void LeaveNoClick()
    {
        askLeave.SetActive(false);
    }
}

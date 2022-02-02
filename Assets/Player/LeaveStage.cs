using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveStage : MonoBehaviour
{
    //������
    public GameObject askLeave;

    //���ӿ���
    public GameObject gameOverCanvas;
    public GameObject itemLostTxt;
    public GameObject itemProtectedTxt;

 

    public void GameOver(bool itemProtected)
    {
        gameOverCanvas.SetActive(true);
        if (itemProtected)
        {
            itemProtectedTxt.SetActive(true);
        }
        else
        {
            itemLostTxt.SetActive(true);
            GameManager.Instance.myItems.Clear();
        }

    }

    public void LeaveBtnClick()
    {
        askLeave.SetActive(true);
    }
    public void LeaveYesClick()
    {
        GameManager.Instance.curSaveData.curStageMap = null;
        SceneManager.LoadScene("LobbyScene");
    }
    public void LeaveNoClick()
    {
        askLeave.SetActive(false);
    }
}
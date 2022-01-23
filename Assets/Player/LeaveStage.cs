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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        SceneManager.LoadScene("LobbyScene");
    }
    public void LeaveNoClick()
    {
        askLeave.SetActive(false);
    }
}

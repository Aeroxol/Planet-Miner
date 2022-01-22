using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveStage : MonoBehaviour
{
    public GameObject askLeave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

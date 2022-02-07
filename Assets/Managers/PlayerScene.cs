using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScene : MonoBehaviour
{
    // singleton
    private static PlayerScene instance = null;

    public static PlayerScene Instance
    {
        get
        {
            if(null == instance)
            {
                Debug.Log("PlayerScene not instantiated");
                return null;
            }
            return instance;
        }
    }

    public Stage stage;
    public PlayerManager player;
    public MessageBoxManager messageBox;
    public Button btnMenu;
    public GameObject helpCanvas;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (GameManager.Instance.curSaveData.curStageMap == null)
        {
            //new
            stage.Generate(GameManager.Instance.curSaveData.curStageData);
            player.SetStats(false);
        }
        else
        {
            //load
            stage.RenderStage(GameManager.Instance.curSaveData.curStageData, GameManager.Instance.curSaveData.curStageMap);
            player.SetStats(true);
        }
    }

    private void Start()
    {
        player.GetComponent<Rigidbody2D>().simulated = true;
        GameManager.Instance.loadingManager.LoadingComplete();

        if (GameManager.Instance.curSaveData.isFirstSage)
        {
            helpCanvas.SetActive(true);
        }
    }

    public void BtnQuit()
    {
        GameManager.Instance.curSaveData.playerHp = player.hp;
        GameManager.Instance.curSaveData.playerX = player.transform.position.x;
        GameManager.Instance.curSaveData.playerY = player.transform.position.y;
        GameManager.Save();
        messageBox.ShowMessageBox("저장되었습니다.");
        /*
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
       */
    }

    public void helpCloseClick()
    {
        GameManager.Instance.curSaveData.isFirstSage = false;
        helpCanvas.SetActive(false);
    }
}

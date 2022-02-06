using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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

    private void Awake()
    {
        if (null == instance)
        {
            Debug.Log("is this not happening?");
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
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

        Invoke("SetDefaultSprites", 1.0f);
        player.GetComponent<Rigidbody2D>().simulated = true;
    }

    void SetDefaultSprites()
    {
        for (int i = 0; i < stage.blocks.Count; i++)
        {
            stage.blocks[i].SetDefaultSprite();
        }
        GameManager.Instance.loadingManager.LoadingComplete();
    }

    public void BtnQuit()
    {
        GameManager.Instance.curSaveData.playerHp = player.hp;
        GameManager.Instance.curSaveData.playerX = player.transform.position.x;
        GameManager.Instance.curSaveData.playerY = player.transform.position.y;
        SaveData.Save(GameManager.Instance.curSaveData);
        messageBox.ShowMessageBox("저장되었습니다.");
        /*
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
       */
    }
}

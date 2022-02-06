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
    public Button btnMenu;

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

        if (GameManager.Instance.curSaveData.curStageMap == null)
        {
            //new
            stage.Generate(GameManager.Instance.curSaveData.curStageData);
        }
        else
        {
            //load
            stage.RenderStage(GameManager.Instance.curSaveData.curStageData, GameManager.Instance.curSaveData.curStageMap);
        }
    }

    private void Start()
    {
        btnMenu.onClick.AddListener(GameManager.Instance.BtnOption);
        Invoke("SetDefaultSprites", 1.0f);
    }

    void SetDefaultSprites()
    {
        for (int i = 0; i < stage.blocks.Count; i++)
        {
            stage.blocks[i].SetDefaultSprite();
        }
        GameManager.Instance.loadingManager.LoadingComplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerScene : MonoBehaviour
{
    public Stage stage;
    private void Start()
    {
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

    public void BtnQuit()
    {
        SaveData.Save(GameManager.Instance.curSaveData);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

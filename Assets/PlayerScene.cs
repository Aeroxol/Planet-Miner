using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScene : MonoBehaviour
{


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

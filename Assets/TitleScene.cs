using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class TitleScene : MonoBehaviour
{
    public GameObject savePanel;
    public GameObject saveContent;
    public SaveSlot saveSlot;
    public NewGameSlot ngs;
    [HideInInspector]
    public string saveName;
    // Start is called before the first frame update
    void Start()
    {
        GetSaveFiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            savePanel.SetActive(true);
        }
    }

    public void GetSaveFiles()
    {
        var saveDir = Directory.CreateDirectory(Application.dataPath + "/Save");
        var fileInfo = saveDir.GetFiles();
        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json")
            {
                SaveSlot newSaveSlot = GameObject.Instantiate<SaveSlot>(saveSlot, saveContent.transform);
                newSaveSlot.nameText.text = Path.GetFileNameWithoutExtension(file.Name);
            }
        }
        ngs.transform.SetAsLastSibling();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class TitleScene : MonoBehaviour
{
    public GameObject savePanel;
    public GameObject saveContent;
    public SaveSlot saveSlot;
    public NewGameSlot ngs;
    public GameObject deletePanel;
    public Text deleteText;
    public GameObject touchToStart;

    public List<SaveSlot> saveSlotList = new List<SaveSlot>();
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
            touchToStart.SetActive(false);
        }
    }

    public void GetSaveFiles()
    {
        foreach(SaveSlot obj in saveSlotList)
        {
            Destroy(obj.gameObject);
        }
        saveSlotList.Clear();
        var saveDir = Directory.CreateDirectory(Application.dataPath + "/Save");
        var fileInfo = saveDir.GetFiles();
        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json")
            {
                SaveSlot newSaveSlot = GameObject.Instantiate<SaveSlot>(saveSlot, saveContent.transform);
                newSaveSlot.titleScene = this;
                saveSlotList.Add(newSaveSlot);
                newSaveSlot.nameText.text = Path.GetFileNameWithoutExtension(file.Name);
            }
        }
        ngs.transform.SetAsLastSibling();
    }

    public void BtnDelete()
    {
        deletePanel.gameObject.SetActive(false);
        File.Delete(Application.dataPath + "/Save/" + deleteText.text + ".json");
        File.Delete(Application.dataPath + "/Save/" + deleteText.text + ".json.meta");

        GetSaveFiles();
    }

    public void BtnCancel()
    {
        deletePanel.gameObject.SetActive(false);
    }
}
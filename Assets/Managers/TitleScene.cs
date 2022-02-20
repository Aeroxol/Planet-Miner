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

    public Image titleImage;
    public Image title;
    bool titleAnimationComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        GetSaveFiles();
        StartCoroutine(TitleImageOn());
        SoundManager.Play("title");
    }

    // Update is called once per frame
    void Update()
    {
        if (titleAnimationComplete&&Input.anyKeyDown)
        {
            if (!savePanel.activeSelf)
            {
                SoundManager.Play("click");
            }
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
        var saveDir = Directory.CreateDirectory(Application.persistentDataPath + "/Save");
        var fileInfo = saveDir.GetFiles();
        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json")
            {
                SaveSlot newSaveSlot = GameObject.Instantiate<SaveSlot>(saveSlot, saveContent.transform);
                newSaveSlot.titleScene = this;
                saveSlotList.Add(newSaveSlot);
                newSaveSlot.nameText.text = Path.GetFileNameWithoutExtension(file.Name);
                //newSaveSlot.nameText.text = Path.GetFileNameWithoutExtension(file.Name);
                newSaveSlot.btnStart.onClick.AddListener(delegate { SoundManager.Play("click"); });
                newSaveSlot.btnDelete.onClick.AddListener(delegate { SoundManager.Play("click"); });
            }
        }
        ngs.transform.SetAsLastSibling();
    }

    public void BtnDelete()
    {
        deletePanel.gameObject.SetActive(false);
        File.Delete(Application.persistentDataPath + "/Save/" + deleteText.text + ".json");
        File.Delete(Application.persistentDataPath + "/Save/" + deleteText.text + ".json.meta");

        GetSaveFiles();
    }

    public void BtnCancel()
    {
        deletePanel.gameObject.SetActive(false);
    }

    IEnumerator TitleImageOn()
    {
        while (true)
        {
            float speed = 1.5f;
            titleImage.color = new Color(titleImage.color.r + Time.deltaTime * speed, titleImage.color.g + Time.deltaTime * speed, titleImage.color.b + Time.deltaTime * speed, 1);
            if (titleImage.color.r >= 1)
            {
                titleImage.color = new Color(1, 1, 1, 1);
                break;
            }
            yield return null;
        }
        while (true)
        {
            title.rectTransform.anchoredPosition = new Vector3(title.rectTransform.anchoredPosition.x - Time.deltaTime*1500, 0, 0);
            if (title.rectTransform.anchoredPosition.x <= 0)
            {
                title.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        touchToStart.SetActive(true);
        titleAnimationComplete = true;
    }
}
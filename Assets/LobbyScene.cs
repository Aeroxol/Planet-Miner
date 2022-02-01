using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class LobbyScene : MonoBehaviour
{
    public Canvas generalCanvas;
    public Canvas upgradeCanvas;
    public Canvas planetCanvas;

    public GameObject robotImage;

    private List<StageData> stages = new List<StageData>();
    public ScrollRect scrollRect;
    public List<Image> scrollImages = new List<Image>();
    private int scrollIndex = 0;
    public RectTransform scrollContents;
    private Task scrollTask;

    public Text descLevel;
    public Text descOres;

    private void Awake()
    {
        if (stages.Count == 0)
        {
            for(int i = 0; i < 5; ++i)
            {
                stages.Add(new StageData(i));
                scrollImages[i].sprite = GameManager.Instance.planetImages[stages[i].imageNum];
                UpdateDescription();
            }
        }
    }

    public void SelectPage()
    {
        upgradeCanvas.gameObject.SetActive(false);
        planetCanvas.gameObject.SetActive(true);
        robotImage.SetActive(false);
    }

    public void UpgradePage()
    {
        planetCanvas.gameObject.SetActive(false);
        upgradeCanvas.gameObject.SetActive(true);
        robotImage.SetActive(true);
    }

    public void UpdateDescription()
    {
        descLevel.text = (stages[scrollIndex].level + 1).ToString();
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < stages[scrollIndex].oreIndex.Count; ++i)
        {
            //ToDo
            sb.Append(GameManager.Instance.oreLevelData[stages[scrollIndex].level].ores[stages[scrollIndex].oreIndex[i]].itemName);
            sb.Append(" ");
        }
        descOres.text = sb.ToString();
    }

    public void BtnLeft()
    {
        if (scrollTask == null || scrollTask.IsCompleted)
        {
            scrollIndex = Mathf.Clamp(scrollIndex - 1, 0, 4);
            scrollTask = MoveScroll();
            UpdateDescription();
        }
    }

    public void BtnRight()
    {
        if (scrollTask == null || scrollTask.IsCompleted)
        {
            scrollIndex = Mathf.Clamp(scrollIndex + 1, 0, 4);
            scrollTask = MoveScroll();
            UpdateDescription();
        }
    }

    //����
    public async Task MoveScroll()
    {
        float startTime = Time.time;
        while (true)
        {
            await Task.Yield();
            if (Mathf.Abs(scrollContents.localPosition.x - scrollIndex * -700) < 1)
            {
                scrollContents.localPosition = new Vector3(scrollIndex * -700, 0, 0);
                break;
            }
            scrollContents.localPosition = new Vector3(Mathf.Lerp(scrollContents.localPosition.x, scrollIndex * -700, 12 *Time.deltaTime), 0, 0);
        }
    }

    public void BtnStart()
    {
        GameManager.Instance.curSaveData.curStageData = stages[scrollIndex];
        SceneManager.LoadScene(2);
    }

    public void BtnSave()
    {
        SaveData.Save(GameManager.Instance.curSaveData);
    }
}

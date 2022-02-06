using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Manager Instances
    private static GameManager instance = null;
    public LoadingManager loadingManager;

    // Stage Generator Property
    public List<BlockData> blocks = new List<BlockData>();
    public List<BlockData> dirtBlocks = new List<BlockData>();
    public List<Sprite> planetImages = new List<Sprite>();

    // Option
    public Canvas optionCanvas;
    public Slider volumeSlider;
    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> data;
    }
    public List<ListWrapper<OreData>> oreLevelData = new List<ListWrapper<OreData>>();
    public OreData gold;
    public OreData uranium;
    public List<ListWrapper<float>> stageLevelProbs = new List<ListWrapper<float>>();

    [HideInInspector] public UpgradeInfo upgradeInfo = new UpgradeInfo();//업그레이드 수치 데이터
    public BlockSprites[] blockLevel = new BlockSprites[7]; //블록 스프라이트 1~7레벨
    public Sprite[] cracks = new Sprite[7]; //블록 크랙 스프라이트
   // public List<ItemInSlot> myItems = new List<ItemInSlot>();//플레이어가 가지고있는 아이템

    public SaveData curSaveData;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //volumeSlider.onValueChanged.AddListener(delegate { SoundManager.SetVolume(volumeSlider.value); });
    }

    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void BtnOption()
    {
        optionCanvas.gameObject.SetActive(true);
    }
    public void BtnOptionClose()
    {
        optionCanvas.gameObject.SetActive(false);
    }
    public static void Save()
    {
        SaveData.Save(GameManager.Instance.curSaveData);
    }

    public static void SaveExit()
    {
        SaveData.Save(GameManager.Instance.curSaveData);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

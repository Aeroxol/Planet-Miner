using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Manager Instances
    private static GameManager instance = null;
    public LoadingManager loadingManager;
    public SoundManager soundManager;

    // Stage Generator Property
    public List<BlockData> blocks = new List<BlockData>();
    public List<BlockData> dirtBlocks = new List<BlockData>();
    public List<Sprite> planetImages = new List<Sprite>();

    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> data;
    }
    public List<ListWrapper<OreData>> oreLevelData = new List<ListWrapper<OreData>>();
    public OreData gold;
    public OreData uranium;
    public List<ListWrapper<float>> stageLevelProbs = new List<ListWrapper<float>>();

    [HideInInspector] public UpgradeInfo upgradeInfo = new UpgradeInfo();//���׷��̵� ��ġ ������
    public BlockSprites[] blockLevel = new BlockSprites[7]; //���� ��������Ʈ 1~7����
    public Sprite[] cracks = new Sprite[7]; //���� ũ�� ��������Ʈ
   // public List<ItemInSlot> myItems = new List<ItemInSlot>();//�÷��̾ �������ִ� ������

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
}
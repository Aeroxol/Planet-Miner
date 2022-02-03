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

    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> ores;
    }
    public List<ListWrapper<OreData>> oreLevelData = new List<ListWrapper<OreData>>();

    [HideInInspector] public UpgradeInfo upgradeInfo = new UpgradeInfo();//���׷��̵� ��ġ ������
    public BlockSprites[] blockLevel = new BlockSprites[7]; //��� ��������Ʈ 1~7����
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

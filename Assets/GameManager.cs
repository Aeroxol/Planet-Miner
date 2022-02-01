using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
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


    public List<ItemInSlot> myItems = new List<ItemInSlot>();//플레이어가 가지고있는 아이템
    public int myMoney; //돈

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

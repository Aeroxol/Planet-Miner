using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    // Stage Generator Property
    public List<BlockData> blocks = new List<BlockData>();
    public List<Sprite> planetImages = new List<Sprite>();
    public List<StageData> stageLevelData = new List<StageData>();

    public StageData curStage;

    public List<ItemInSlot> myItems = new List<ItemInSlot>();//플레이어가 가지고있는 아이템

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void BtnSave()
    {

    }
}

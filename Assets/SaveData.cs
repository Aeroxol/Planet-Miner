using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveData
{
    // Save Data Property
    // =====저장하고 싶은 정보는 여기에 다 적으면 됩니다.=====
    public string saveName;
    public int[,] curStageMap;

    public StageData curStageData;

    public int myMoney = 0; //돈
    public List<ItemInSlot> myItems = new List<ItemInSlot>();//플레이어가 가지고있는 아이템
    public int[] myUpgradeLvs = { 1, 1, 1, 1, 1 }; //현재 업그레이드 레벨 Dig-Booster-Hp-Inven-Resist
    public int myShipLv = 1; //우주선 레벨
    public int[] myShipMaterials = { 0, 0, 0, 0 }; //플레이어가 넣은 우주선 강화재료 개수
    public bool itemProtected = false; //보험 적용 중 여부
    // ====================여기까지====================
    public SaveData(string name)
    {
        saveName = name;
    }
    public static void Save(SaveData data)
    {
        string data2 = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/save/" + data.saveName + ".json", data2);
    }

    public static SaveData Load(string name)
    {
        string data = File.ReadAllText(Application.dataPath + "/save/" + name + ".json");
        return JsonConvert.DeserializeObject<SaveData>(data);
    }
}

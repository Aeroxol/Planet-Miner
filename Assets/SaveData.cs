using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveData
{
    // Save Data Property
    // =====�����ϰ� ���� ������ ���⿡ �� ������ �˴ϴ�.=====
    public string saveName;
    public int[,] curStageMap;

    public StageData curStageData;

    // ====================�������====================
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    // Save Data Property

    // =====저장하고 싶은 정보는 여기에 다 적으면 됩니다.=====
    public string saveName;

    // ====================여기까지====================

    // Save & Load Function
    public SaveData(string name)
    {
        if(File.Exists(Application.dataPath + "/save/" + name + ".json"))
        {
            Load(name);
        }
        else
        {
            saveName = name;
            Save();
        }
    }

    public void Save()
    {
        string data = JsonUtility.ToJson(this);
        File.WriteAllText(Application.dataPath + "/save/" + saveName + ".json", data);
    }

    public void Load(string name)
    {
        string data = File.ReadAllText(Application.dataPath + "/save/" + name + ".json");
        JsonUtility.FromJsonOverwrite(data, this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    // Save Data Property

    // =====�����ϰ� ���� ������ ���⿡ �� ������ �˴ϴ�.=====
    public string saveName;

    // ====================�������====================

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

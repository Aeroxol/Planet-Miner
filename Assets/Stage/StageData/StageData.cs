using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class StageData
{
    public int level;
    //public Sprite image;
    public int imageNum;
    public int width, height;
    public List<int> depth = new List<int>();
    public List<int> oreIndex = new List<int>();
    public int mixCount;
    public int disNum;
    public int disLength;
    public List<AnimationCurve> oreProbs = new List<AnimationCurve>();

    /*
    public static StageData NewStage(int level)
    {
        StageData newStageData = ScriptableObject.CreateInstance<StageData>();
        newStageData.level = level;
        newStageData.width = (int)RandomGaussian(60, 80);
        newStageData.height = (int)RandomGaussian(150, 250);
        // dirt
        newStageData.blocks = new List<BlockData>(GameManager.Instance.stageLevelData[level].blocks);

        newStageData.depth.Add(Random.Range(newStageData.height * 3 / 12, newStageData.height * 5 / 12));
        newStageData.depth.Add(Random.Range(newStageData.height * 7 / 12, newStageData.height * 9 / 12));
        newStageData.depth.Add(newStageData.height + 1);

        // ores
        int oresNum = Random.Range(3, Mathf.Min(5, GameManager.Instance.stageLevelData[level].ores.Count - 2)); ;
        List<OreData> tempOres = new List<OreData>(GameManager.Instance.stageLevelData[level].ores);
        for (int i = 0; i < oresNum; ++i)
        {
            int index = Random.Range(0, tempOres.Count - 2);
            newStageData.ores.Add(tempOres[index]);
            newStageData.oreProbs.Add(tempOres[index].probabilityGraphs[Random.Range(0, tempOres[index].probabilityGraphs.Count)]);
            tempOres.RemoveAt(index);
        }
        // gold
        newStageData.ores.Add(tempOres[tempOres.Count - 2]);
        newStageData.oreProbs.Add(tempOres[tempOres.Count - 2].probabilityGraphs[Random.Range(0, tempOres[tempOres.Count - 2].probabilityGraphs.Count)]);
        // uranium
        newStageData.ores.Add(tempOres[tempOres.Count - 1]);
        newStageData.oreProbs.Add(tempOres[tempOres.Count - 1].probabilityGraphs[Random.Range(0, tempOres[tempOres.Count - 1].probabilityGraphs.Count)]);

        newStageData.mixCount = Random.Range(10, 20);
        newStageData.disNum = Random.Range(5, 25);
        newStageData.disLength = Random.Range(5, 15);
        newStageData.image = GameManager.Instance.planetImages[Random.Range(0, GameManager.Instance.planetImages.Count)];
        return newStageData;
    }*/

    public StageData(int _level)
    {
        level = _level;
        width = (int)RandomGaussian(60, 80);
        height = (int)RandomGaussian(150, 250);
        // dirt
        depth.Add(Random.Range(height * 3 / 12, height * 5 / 12));
        depth.Add(Random.Range(height * 7 / 12, height * 9 / 12));
        depth.Add(height + 1);

        // ores
        // 레벨의 광물종류
        int stageOreNum = GameManager.Instance.oreLevelData[_level].ores.Count;
        // 스테이지에 등장할 광물의 수
        int oreNum = Random.Range(3, Mathf.Min(6, stageOreNum - 2));
        List<int> numPool = new List<int>();
        for (int i = 0; i < stageOreNum - 2; ++i)
        {
            numPool.Add(i);
        }
        for(int i = 0; i < oreNum; ++i)
        {
            int temp = Random.Range(0, numPool.Count);
            int index = numPool[temp];
            oreIndex.Add(index);
            oreProbs.Add(GameManager.Instance.oreLevelData[_level].ores[index].probabilityGraphs[Random.Range(0, GameManager.Instance.oreLevelData[_level].ores[index].probabilityGraphs.Count)]);
            numPool.RemoveAt(temp);
        }
        //gold
        oreIndex.Add(stageOreNum - 2);
        oreProbs.Add(GameManager.Instance.oreLevelData[_level].ores[stageOreNum - 2].probabilityGraphs[Random.Range(0, GameManager.Instance.oreLevelData[_level].ores[stageOreNum - 2].probabilityGraphs.Count)]);
        //uranium
        oreIndex.Add(stageOreNum - 1);
        oreProbs.Add(GameManager.Instance.oreLevelData[_level].ores[stageOreNum - 1].probabilityGraphs[Random.Range(0, GameManager.Instance.oreLevelData[_level].ores[stageOreNum - 1].probabilityGraphs.Count)]);

        mixCount = Random.Range(10, 20);
        disNum = Random.Range(5, 25);
        disLength = Random.Range(5, 15);
        imageNum = Random.Range(0, GameManager.Instance.planetImages.Count);
    }
    public static float RandomGaussian(float minValue = -1f, float maxValue = 1f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
}

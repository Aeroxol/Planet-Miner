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
    public AnimationCurve goldProb;
    public int uraniumNum;
    bool isHereUnobtainium = false;

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
        int stageOreNum = GameManager.Instance.oreLevelData[_level].data.Count;
        // 스테이지에 등장할 광물의 수
        int oreNum = Random.Range(3, Mathf.Min(5, stageOreNum));
        List<int> numPool = new List<int>();
        for (int i = 0; i < stageOreNum; ++i)
        {
            numPool.Add(i);
        }
        for(int i = 0; i < oreNum; ++i)
        {
            int temp = Random.Range(0, numPool.Count);
            int index = numPool[temp];
            oreIndex.Add(index);
            oreProbs.Add(GameManager.Instance.oreLevelData[_level].data[index].probabilityGraphs[Random.Range(0, GameManager.Instance.oreLevelData[_level].data[index].probabilityGraphs.Count)]);
            numPool.RemoveAt(temp);
            if ((index == 8) && (_level == 4)) isHereUnobtainium = true;
        }
        //gold
        goldProb = GameManager.Instance.gold.probabilityGraphs[Random.Range(0, GameManager.Instance.gold.probabilityGraphs.Count)];
        uraniumNum = Random.Range(3, 2 + _level * 2);
        if (isHereUnobtainium) uraniumNum = 10;
        mixCount = Random.Range(10, 20);
        switch (_level)
        {
            case 0:
                disNum = Random.Range(0, 50);
                break;
            case 1:
                disNum = Random.Range(150, 250);
                break;
            case 2:
                disNum = Random.Range(250, 350);
                break;
            case 3:
                disNum = Random.Range(350, 450);
                break;
            case 4:
                disNum = Random.Range(450, 550);
                break;
        }
        if (isHereUnobtainium) disNum = Random.Range(750, 850);
        //disNum = Random.Range(5, 25);
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

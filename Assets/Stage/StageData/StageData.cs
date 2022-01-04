using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage Set")]
public class StageData : ScriptableObject
{
    public Sprite image;
    public int width, height;
    public List<BlockData> blocks = new List<BlockData>();
    public List<int> depth = new List<int>();
    public List<OreData> ores = new List<OreData>();
    public int mixCount;
    public int disNum;
    public int disLength;
    public List<AnimationCurve> oreProbs = new List<AnimationCurve>();

    public static StageData NewStage(int level)
    {
        StageData newStageData = ScriptableObject.CreateInstance<StageData>();
        newStageData.width = (int)RandomGaussian(60, 80);
        newStageData.height = (int)RandomGaussian(150, 250);
        // ¿©±â
        newStageData.blocks.Add(BlockData.Instantiate(GameManager.Instance.dirtBlocks[Mathf.Clamp(level - 1, 0, GameManager.Instance.dirtBlocks.Count-1)]));
        newStageData.blocks.Add(BlockData.Instantiate(GameManager.Instance.dirtBlocks[Mathf.Clamp(level, 0, GameManager.Instance.dirtBlocks.Count-1)]));
        newStageData.blocks.Add(BlockData.Instantiate(GameManager.Instance.dirtBlocks[Mathf.Clamp(level + 1, 0, GameManager.Instance.dirtBlocks.Count-1)]));

        newStageData.depth.Add(Random.Range(newStageData.height * 3/ 12, newStageData.height * 5 / 12));
        newStageData.depth.Add(Random.Range(newStageData.height * 7 / 12, newStageData.height * 9 / 12));
        newStageData.depth.Add(newStageData.height + 1);
        int oresNum = Random.Range(2, GameManager.Instance.ores.Count);
        List<OreData> tempOres = new List<OreData>(GameManager.Instance.ores);
        for (int i = 0; i < oresNum; ++i)
        {
            int index = Random.Range(0, tempOres.Count);
            newStageData.ores.Add(tempOres[index]);
            newStageData.oreProbs.Add(tempOres[index].probabilityGraphs[Random.Range(0, tempOres[index].probabilityGraphs.Count)]);
            tempOres.RemoveAt(index);
        }
        newStageData.mixCount = Random.Range(10, 20);
        newStageData.disNum = Random.Range(5, 25);
        newStageData.disLength = Random.Range(5, 15);
        newStageData.image = GameManager.Instance.planetImages[Random.Range(0, GameManager.Instance.planetImages.Count)];
        return newStageData;
    }

    public static float RandomGaussian(float minValue = -1f, float maxValue = 1f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
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

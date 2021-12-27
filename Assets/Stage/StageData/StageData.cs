using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage Set")]
public class StageData : ScriptableObject
{
    public int width, height;
    public List<BlockData> blocks;
    public List<int> depth;
    public List<OreData> ores;
    public int mixCount;
    public int disNum;
    public int disLength;
    public List<AnimationCurve> oreProbs;
}

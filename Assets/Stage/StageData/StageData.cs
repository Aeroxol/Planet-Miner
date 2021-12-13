using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage Set")]
public class StageData : ScriptableObject
{
    public int width, height;
    public List<BlockData> blocks;
    public List<int> depth;
}

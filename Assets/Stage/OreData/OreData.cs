using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Stage/Ore")]

public class OreData : ScriptableObject
{
    public enum Code
    {
        coal,
        copper,
        iron,
        silver,
        gold,
        uranium,
        lead,
        tungsten,
        aluminium,
        chromium,
        platinium,
        titanium,
        lithium,
        unobtainium,
        item
    }
    public Code id;
    public string editorName;
    public Sprite artwork;
    public int itemCode;
    public string itemName;
    public int maxAmount;
    public bool isUsable;
    public List<AnimationCurve> probabilityGraphs;
    public int price;
    public string description;
}

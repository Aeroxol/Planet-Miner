using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "New Block", menuName = "Stage/Block")]
public class BlockData : ScriptableObject
{
    public bool invincible;
    public int health;
    public int level;
    public Sprite[] artwork;
}

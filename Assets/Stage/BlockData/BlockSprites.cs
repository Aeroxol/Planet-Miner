using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockSprites
{
    // sprite type: 0~11
    // 0~11: (상하좌우, 0=옆에블록없음,1=있음) 0000 0001 0010 0011 0100 0101 0110 0111 1000 1001 1010 1011
    public Sprite[] spriteType;
}
/*
public class BlockSprites : MonoBehaviour
{
    public BlockSpriteContainer[] spriteLevel = new BlockSpriteContainer[7]; //1~7단계
}
*/


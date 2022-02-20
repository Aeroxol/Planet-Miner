using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInfo //업그레이드 수치 데이터
{
    public readonly int maxDigLvl = 7;
    public readonly int[] digPowerList = { 50, 90, 162, 291, 523, 941, 1693 };
    public readonly int[] digPriceList = { 5000, 20000, 50000, 150000, 500000, 1500000 };

    public readonly int maxBoosterLvl = 4;
    public readonly int[] boosterPowerList = { 24, 28, 32, 36 };
    public readonly float[] maxFlyList = { 5.5f, 6.0f, 6.5f, 7.0f };
    public readonly int[] boosterPriceList = { 10000, 75000, 200000 };

    public readonly int maxHpLvl = 5;
    public readonly int[] hpAmountList = { 1000, 2000, 3000, 4000, 5000 };
    public readonly int[] hpPriceList = { 5000, 25000, 100000, 200000 };

    public readonly int maxInvenLvl = 5;
    public readonly int[] invenAmountList = { 20, 24, 28, 32, 36 };
    public readonly int[] invenPriceList = { 10000, 50000, 100000, 150000 };

    public readonly int maxResistLvl = 5;
    public readonly int[] resistAmountList = { 0, 1, 2, 3, 4 };
    public readonly int[] resistPriceList = { 10000, 40000, 200000, 400000 };

    public readonly int maxShipLvl = 5;
    public readonly int[,] materialType = { { 0, 1, 2, 12 }, { 5, 6, 2, 12 }, { 7, 8, 2, 12 }, { 9, 10, 2, 12 } };
    public readonly int[,] materialAmount = { { 100, 100, 50, 1 }, { 100, 100, 100, 2 }, { 150, 150, 200, 4 }, { 150, 150, 400, 8 } }; //실수치
    //public readonly int[,] materialAmount = { { 5, 5, 2, 1 }, { 5, 5, 5, 1 }, { 5, 5, 10, 1 }, { 5, 5, 10, 1 } }; //테스트용 너프수치
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStockData
{
    
    // 地雷の所持数の初期値
    [SerializeField] private int initialMineCount = 0;

    // 所持できる地雷の最大値
    [SerializeField] private int maxMineCount = 3;

    // 地雷カートリッジを取得した時に補充する数
    [SerializeField] private int replenishMineCount = 1;

    // 現在の地雷の所持数（private）
    private int currentMineCount;

    // 現在の地雷の所持数を返す getter（public）
    public int CurrentMineCount
    {
        get { return currentMineCount; }
    }

    // コンストラクタまたは初期化メソッド
    public WeaponStockData()
    {
        InitializeMineCount();
    }

    // 現在所持数を初期化するメソッド
    public void InitializeMineCount()
    {
        currentMineCount = initialMineCount;
    }

    // 現在の所持数を増やす（最大値を超えないようにする）
    public void AddMines(int amount)
    {
        currentMineCount = Mathf.Min(currentMineCount + amount, maxMineCount);
    }

    // 現在の所持数をデクリメントする（ゼロを下回らないようにする）
    public void DecreaseMineCount()
    {
        if (currentMineCount > 0)
        {
            currentMineCount--;
        }
    }
}

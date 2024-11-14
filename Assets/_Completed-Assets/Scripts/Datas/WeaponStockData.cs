using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class WeaponStockData
{
    [SerializeField] private int mineInitial; // 初期所持数
    [SerializeField] private int mineMax; // 最大所持数
    [SerializeField] private int minereplenishment; // 補充量
    private int mineCurrent; // 現在の所持数
    
    // コンストラクタ
    public WeaponStockData(int initial, int max, int replenishment)
    {
        mineInitial = initial;
        mineMax = max;
        minereplenishment = replenishment;
        mineCurrent = mineInitial; // 初期所持数で設定
    }

    // 現在の所持数を返すgetter
    public int GetMineCurrent()
    {
        return mineCurrent;
    }

    // 現在所持数を初期化する
    public void ResetMineCount()
    {
        mineCurrent = mineInitial; // 初期値にリセット
    }

    // 現在の所持数を増やす（最大値を超えないようにする）
    public void IncreaseMineCount(int amount)
    {
        mineCurrent = Mathf.Min(mineCurrent + amount, mineMax); // 増加量が最大値を超えないようにする
    }

    // 現在の所持数をデクリメントする（ゼロを下回らないようにする）
    public void DecreaseMineCount()
    {
        mineCurrent = Mathf.Max(mineCurrent - 1, 0); // 減少量がゼロを下回らないようにする
    }
}

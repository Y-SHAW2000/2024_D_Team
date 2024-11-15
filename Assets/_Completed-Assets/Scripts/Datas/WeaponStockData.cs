using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

[System.Serializable]
public class WeaponStockData
{
    [SerializeField] private int mineInitial = 0;       // 初期地雷の所持数
    [SerializeField] private int mineMax = 3;          // 最大地雷の所持数
    [SerializeField] private int mineReplenishment = 1; // 地雷の補充量
    [SerializeField] private int shellInitial = 10;     // 初期弾の所持数
    [SerializeField] private int shellMax = 30;         // 最大弾の所持数
    [SerializeField] private int shellReplenishment = 10; // 弾の補充量

    public Dictionary<string, int> weaponStock; // 武器名と所持数を管理する辞書
    public Dictionary<string, int> weaponMax;   // 武器の最大所持数を管理する辞書
    public Dictionary<string, int> weaponReplenishment; // 武器の補充量を管理する辞書
    public Dictionary<string, int> weaponInitial;

    // コンストラクタ
    public WeaponStockData()
    {
        weaponStock = new Dictionary<string, int>
        {
            { "Mine", mineInitial },
            { "Shell", shellInitial }
        };
        weaponInitial = new Dictionary<string, int>
        {
            { "Mine", mineInitial },
            { "Shell", shellInitial }
        };

        weaponMax = new Dictionary<string, int>
        {
            { "Mine", mineMax },
            { "Shell", shellMax }
        };

        weaponReplenishment = new Dictionary<string, int>
        {
            { "Mine", mineReplenishment },
            { "Shell", shellReplenishment }
        };
    }

    // 現在の所持数を取得
    public int GetCurrent(string weapon)
    {
        return weaponStock[weapon];
    }

    // 現在の所持数を初期化
    public void ResetCount(string weapon)
    {
        if (weaponStock.ContainsKey(weapon))
        {
            weaponStock[weapon] = weapon == "Mine" ? mineInitial : shellInitial;
        }
    }

    // 現在の所持数を増加（最大値を超えない）
    public void IncreaseCount(string weapon, int amount)
    {
        if (weaponStock.ContainsKey(weapon))
        {
            weaponStock[weapon] = Mathf.Min(weaponStock[weapon] + amount, weaponMax[weapon]);
        }
    }

    // 現在の所持数を減少（ゼロを下回らない）
    public void DecreaseCount(string weapon)
    {
        if (weaponStock.ContainsKey(weapon))
        {
            weaponStock[weapon] = Mathf.Max(weaponStock[weapon] - 1, 0);
        }
    }

    // 武器の補充
    public void Replenish(string weapon)
    {
        if (weaponReplenishment.ContainsKey(weapon))
        {
            IncreaseCount(weapon, weaponReplenishment[weapon]);
        }
    }
}

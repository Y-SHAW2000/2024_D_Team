using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CartridgeData
{
    // カートリッジのプレハブの参照
    public GameObject cartridgePrefab;

    // カートリッジを生成する位置
    public Vector3 spawnPosition;

    // カートリッジの生成頻度（秒単位）
    public float spawnFrequency;
}

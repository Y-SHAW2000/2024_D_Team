using System;
using UnityEngine;

public class ItemManager: MonoBehaviour
{
    public string userId;
    private void Start()
    {
        // イベントを追加
        GetItem.OnButtonClicked += HandleItemReceived;
    }

    private void HandleItemReceived(GameObject ItemButtonGameObject)
    {
        Debug.Log($"イベントを受け取りました: アイテム名: {ItemButtonGameObject}");
    }
}

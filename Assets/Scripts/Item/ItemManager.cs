using System;
using UnityEngine;

public class ItemManager: MonoBehaviour
{
    public string userId;
    private void Start()
    {
        // �C�x���g��ǉ�
        GetItem.OnButtonClicked += HandleItemReceived;
    }

    private void HandleItemReceived(GameObject ItemButtonGameObject)
    {
        Debug.Log($"�C�x���g���󂯎��܂���: �A�C�e����: {ItemButtonGameObject}");
    }
}

using System;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    private DialogManager dialog;

    public Button ItemButton;  // アイテムのボタン

    private void Awake()
    {
        dialog = FindObjectOfType<DialogManager>();
    }
    private void Start()
    {
        // ボタンのクリックイベントを設定
        ItemButton.onClick.AddListener(() => ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"ボタンが押されました: {ItemButtonGameObject.name}");
        dialog.ShowDialog(ItemButtonGameObject);
    }
}
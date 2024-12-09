using System;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    private DialogManager dialog;
    public static event Action<GameObject> OnButtonClicked;

    public Button ItemButton;  // アイテムのボタン

    private void Awake()
    {
        dialog = FindObjectOfType<DialogManager>();
        if (dialog == null)
        {
            Debug.LogError("DialogManager が見つかりません！シーンに配置されていることを確認してください。");
        }
    }
    private void Start()
    {
        ItemButton.onClick.AddListener(ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"ボタンが押されました: {ItemButtonGameObject.name}");
        dialog.ShowDialog(ItemButtonGameObject);
    }
}
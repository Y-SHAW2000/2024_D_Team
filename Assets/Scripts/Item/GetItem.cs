using System;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public static event Action<GameObject> OnButtonClicked;

    public Button ItemButton;  // アイテムのボタン

    private void Start()
    {
        ItemButton.onClick.AddListener(() => ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"ボタンが押されました: {ItemButtonGameObject.name}");
        OnButtonClicked?.Invoke(ItemButtonGameObject);
    }
}

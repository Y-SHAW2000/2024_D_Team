using System;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public static event Action<GameObject> OnButtonClicked;

    public Button ItemButton;  // �A�C�e���̃{�^��

    private void Start()
    {
        ItemButton.onClick.AddListener(() => ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"�{�^����������܂���: {ItemButtonGameObject.name}");
        OnButtonClicked?.Invoke(ItemButtonGameObject);
    }
}

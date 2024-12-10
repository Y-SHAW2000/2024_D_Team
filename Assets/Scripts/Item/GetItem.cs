using System;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    private DialogManager dialog;

    public Button ItemButton;  // �A�C�e���̃{�^��

    private void Awake()
    {
        dialog = FindObjectOfType<DialogManager>();
    }
    private void Start()
    {
        // �{�^���̃N���b�N�C�x���g��ݒ�
        ItemButton.onClick.AddListener(() => ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"�{�^����������܂���: {ItemButtonGameObject.name}");
        dialog.ShowDialog(ItemButtonGameObject);
    }
}
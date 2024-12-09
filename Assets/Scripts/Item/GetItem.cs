using System;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    private DialogManager dialog;
    public static event Action<GameObject> OnButtonClicked;

    public Button ItemButton;  // �A�C�e���̃{�^��

    private void Awake()
    {
        dialog = FindObjectOfType<DialogManager>();
        if (dialog == null)
        {
            Debug.LogError("DialogManager ��������܂���I�V�[���ɔz�u����Ă��邱�Ƃ��m�F���Ă��������B");
        }
    }
    private void Start()
    {
        ItemButton.onClick.AddListener(ButtonClicked(ItemButton.gameObject));
    }

    private void ButtonClicked(GameObject ItemButtonGameObject)
    {
        Debug.Log($"�{�^����������܂���: {ItemButtonGameObject.name}");
        dialog.ShowDialog(ItemButtonGameObject);
    }
}
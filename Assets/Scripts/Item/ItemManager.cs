using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private UserStateManager userStateManager;
    private ItemInfo iteminfo;
    private int armor;
    private int stamina;

    private void OnEnable()
    {
        // �A�C�e���ύX�C�x���g���w��
        GetItem.OnButtonClicked += HandleItemReceived;
    }

    private void OnDisable()
    {
        // �A�C�e���ύX�C�x���g�̍w�ǂ�����
        GetItem.OnButtonClicked -= HandleItemReceived;
    }

    void Start()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
        iteminfo = FindObjectOfType<ItemInfo>();
    }
    public void HandleItemReceived(GameObject ItemButtonGameObject)
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null)
        {
            stamina = playerinfo.StaminaItem;
            armor = playerinfo.ArmorItem;

            if (ItemButtonGameObject.name == "Defence_Use_Button" && armor >= 1)
            {
                playerinfo.ArmorItem -= 1;
            }
            if (ItemButtonGameObject.name == "Stamina_Use_Button" && armor >= 1)
            {
                playerinfo.StaminaItem -= 1;
            }
            Debug.Log($"�A�C�e�����g�p����armor : {armor} stamina: {stamina}");
        }
        userStateManager.SavePlayerinfo(playerinfo); //�f�[�^�̍X�V
        iteminfo.UpdateText(armor, stamina);
    }
}

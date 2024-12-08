using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private UserStateManager userStateManager;
    private ItemInfo iteminfo;
    //private UseItemManager useitem;
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
        //useitem = FindObjectOfType<UseItemManager>();
    }
    public void HandleItemReceived(GameObject ItemButtonGameObject)
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null)
        {

            if (ItemButtonGameObject.name == "Defence_Use_Button" && playerinfo.ArmorItem >= 1)
            {
                playerinfo.ArmorItem -= 1;
                UseItemManager.UseArmorItem();
            }
            if (ItemButtonGameObject.name == "Stamina_Use_Button" && playerinfo.StaminaItem >= 1)
            {
                playerinfo.StaminaItem -= 1;
                UseItemManager.UseStaminaItem();
            }
            Debug.Log($"�A�C�e�����g�p����armor : {playerinfo.ArmorItem} stamina: {playerinfo.StaminaItem}");
        }
        userStateManager.SavePlayerinfo(playerinfo); //�f�[�^�̍X�V
        iteminfo.UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
    }
}

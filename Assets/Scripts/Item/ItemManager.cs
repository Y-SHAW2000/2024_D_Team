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
        // アイテム変更イベントを購読
        GetItem.OnButtonClicked += HandleItemReceived;
    }

    private void OnDisable()
    {
        // アイテム変更イベントの購読を解除
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
            Debug.Log($"アイテムを使用したarmor : {playerinfo.ArmorItem} stamina: {playerinfo.StaminaItem}");
        }
        userStateManager.SavePlayerinfo(playerinfo); //データの更新
        iteminfo.UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
    }
}

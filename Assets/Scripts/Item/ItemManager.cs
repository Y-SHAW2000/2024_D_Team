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
            Debug.Log($"アイテムを使用したarmor : {armor} stamina: {stamina}");
        }
        userStateManager.SavePlayerinfo(playerinfo); //データの更新
        iteminfo.UpdateText(armor, stamina);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemInfo : MonoBehaviour
{
    private UserStateManager userStateManager;
    private LoginBonusManager loginBonusManager;

    private int loginDays;
    private int stamina;
    private int armor;

    public Text armorText;
    public Text staminaText;


    IEnumerator Start()
    {

        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager が見つかりません！");
            yield break;
        }

        loginBonusManager = FindObjectOfType<LoginBonusManager>();
        if (loginBonusManager == null)
        {
            Debug.LogError("LoginBonusManager が見つかりません！");
            yield break;
        }

        while (userStateManager.CurrentPlayer == null)
        {
            Debug.Log("CurrentPlayer設定中");
            yield return null;
        }

        Debug.Log("LoginBonusDisplay: CurrentPlayer を認識しました！");

        var playerinfo = userStateManager.CurrentPlayer;
        loginDays = playerinfo.Loginday;
        stamina = playerinfo.StaminaItem;
        armor = playerinfo.ArmorItem;

        FetchLoginDays();   
    }
    
    private void FetchLoginDays()
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null&& UseItemManager.loginbonus == true)
        {
            // 両方のアイテムを +1 増加
            playerinfo.StaminaItem += 1;
            playerinfo.ArmorItem += 1;

            Debug.Log($"ログインにより StaminaItem と ArmorItem を +1 しました。現在の StaminaItem: {playerinfo.StaminaItem}, ArmorItem: {playerinfo.ArmorItem}");

            // データの更新
            userStateManager.SavePlayerinfo(playerinfo);
            Debug.Log($"更新しましたスタミナ: {playerinfo.StaminaItem}アーマー: {playerinfo.ArmorItem}");

            // テキストの更新
            UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
            UseItemManager.loginbonus = false; //ログインボーナスは受け取り済み
        }
        else if(playerinfo != null && UseItemManager.loginbonus == false)
        {
            Debug.Log("ログインボーナスはもう受け取ったよ");
            // データの更新
            userStateManager.SavePlayerinfo(playerinfo);
            Debug.Log($"更新しましたスタミナ: {playerinfo.StaminaItem}アーマー: {playerinfo.ArmorItem}");

            // テキストの更新
            UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
        }
        else
        {
            Debug.LogError("CurrentPlayer が設定されていません！");
        }
    }


    public void UpdateText(int armor, int stamina)
    {
        Debug.Log($"Update stamina: {stamina},armor: {armor}");

        // テキストが設定されている場合に更新
        if (staminaText != null)
        {
            staminaText.text = $"{stamina}";
        }
        else
        {
            Debug.LogError("StaminaText が設定されていません！");
        }

        if (armorText != null)
        {
            armorText.text = $"{armor}";
        }
        else
        {
            Debug.LogError("ArmorText が設定されていません！");
        }
    }
}

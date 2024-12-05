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
    
    private void Getinfo()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        loginDays = playerinfo.Loginday;
        stamina = playerinfo.StaminaItem;
        armor = playerinfo.ArmorItem;
    }
    private void FetchLoginDays()
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null)
        {
            loginDays = playerinfo.Loginday;
            stamina = playerinfo.StaminaItem;
            armor = playerinfo.ArmorItem;

            // ログイン日数に応じた処理
            if (loginDays % 2 == 0) // 偶数の場合
            {
                stamina++;
                Debug.Log($"ログイン日数が偶数なので StaminaItem を +1 しました。現在の StaminaItem: {stamina}");
            }
            else // 奇数の場合
            {
                armor++;
                Debug.Log($"ログイン日数が奇数なので ArmorItem を +1 しました。現在の ArmorItem: {armor}");
            }
            userStateManager.SavePlayerinfo(playerinfo);　//データの更新
            Debug.Log($"更新しましたスタミナ: {stamina}アーマー: {armor}");
            UpdateText(armor, stamina);
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

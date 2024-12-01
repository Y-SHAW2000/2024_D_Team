using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UserStateManager : MonoBehaviour
{
    [Serializable]
    public class Playerinfo
    {
        public string UserId;
        public string UserName;
        public int StaminaItem;
        public int ArmorItem;
        public DateTime LastLoginTime;
        public int Loginday;

        public Playerinfo(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            StaminaItem = 0;
            ArmorItem = 0;
            LastLoginTime = DateTime.MinValue; // 初期値
            Loginday = 0;
        }
    }

    private Playerinfo currentPlayer;

    public Playerinfo CurrentPlayer => currentPlayer;

    void Start()
    {
        string userId = PhotonNetwork.AuthValues.UserId;
        string userName = PhotonNetwork.NickName;

        currentPlayer = LoadPlayerinfo(userId) ?? CreatePlayer(userId, userName);

        Debug.Log($"UserStateManager: プレイヤー情報が設定されました - ID: {currentPlayer.UserId}, Name: {currentPlayer.UserName}");

        SavePlayerinfo(currentPlayer); // ゲーム開始時に一度保存
    }

    private Playerinfo CreatePlayer(string userId, string userName)
    {
        var newPlayer = new Playerinfo(userId, userName);
        Debug.Log($"新しいプレイヤーを作成しました: ID = {userId}, Name = {userName}");
        return newPlayer;
    }

    public void SavePlayerinfo(Playerinfo player)
    {
        PlayerPrefs.SetString($"Player_{player.UserId}_UserName", player.UserName);
        PlayerPrefs.SetInt($"Player_{player.UserId}_StaminaItem", player.StaminaItem);
        PlayerPrefs.SetInt($"Player_{player.UserId}_ArmorItem", player.ArmorItem);
        PlayerPrefs.SetString($"Player_{player.UserId}_LastLoginTime", player.LastLoginTime.ToString("o"));
        PlayerPrefs.SetInt($"Player_{player.UserId}_Loginday", player.Loginday);
        PlayerPrefs.Save();
        Debug.Log($"Playerinfo を保存しました: ID = {player.UserId}");
    }

    private Playerinfo LoadPlayerinfo(string userId)
    {
        if (PlayerPrefs.HasKey($"Player_{userId}_UserName"))
        {
            string userName = PlayerPrefs.GetString($"Player_{userId}_UserName");
            int staminaItem = PlayerPrefs.GetInt($"Player_{userId}_StaminaItem");
            int armorItem = PlayerPrefs.GetInt($"Player_{userId}_ArmorItem");
            string lastLoginTimeStr = PlayerPrefs.GetString($"Player_{userId}_LastLoginTime");
            DateTime lastLoginTime = DateTime.Parse(lastLoginTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind);
            int loginday = PlayerPrefs.GetInt($"Player_{userId}_Loginday");

            Debug.Log($"Playerinfo を読み込みました: ID = {userId}, Name = {userName}");

            return new Playerinfo(userId, userName)
            {
                StaminaItem = staminaItem,
                ArmorItem = armorItem,
                LastLoginTime = lastLoginTime,
                Loginday = loginday
            };
        }

        Debug.Log($"Playerinfo が見つかりません: ID = {userId}");
        return null;
    }
}

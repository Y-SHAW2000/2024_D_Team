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
        public DateTime PreviousLoginTime; // 前回のログイン日時に変更
        public int Loginday;

        public Playerinfo(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            StaminaItem = 0;
            ArmorItem = 0;
            LastLoginTime = DateTime.MinValue; // 初期値
            PreviousLoginTime = DateTime.MinValue; // 初期値
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

        Debug.Log($"[START] プレイヤー情報: ID: {currentPlayer.UserId}, Name: {currentPlayer.UserName}, LastLogin: {currentPlayer.LastLoginTime}, PreviousLogin: {currentPlayer.PreviousLoginTime}, Loginday: {currentPlayer.Loginday}");

        SavePlayerinfo(currentPlayer); // ゲーム開始時に一度保存
    }

    private Playerinfo CreatePlayer(string userId, string userName)
    {
        var newPlayer = new Playerinfo(userId, userName);
        Debug.Log($"[CREATE] 新しいプレイヤー作成: ID = {userId}, Name = {userName}");
        return newPlayer;
    }

    public void SavePlayerinfo(Playerinfo player)
    {
        PlayerPrefs.SetString($"Player_{player.UserId}_UserName", player.UserName);
        PlayerPrefs.SetInt($"Player_{player.UserId}_StaminaItem", player.StaminaItem);
        PlayerPrefs.SetInt($"Player_{player.UserId}_ArmorItem", player.ArmorItem);
        PlayerPrefs.SetString($"Player_{player.UserId}_LastLoginTime", player.LastLoginTime.ToString("o"));
        PlayerPrefs.SetString($"Player_{player.UserId}_PreviousLoginTime", player.PreviousLoginTime.ToString("o")); // 保存時に変更
        PlayerPrefs.SetInt($"Player_{player.UserId}_Loginday", player.Loginday);
        PlayerPrefs.Save();

        Debug.Log($"[SAVE] プレイヤー情報を保存しました: ID: {player.UserId}, Name: {player.UserName}, LastLogin: {player.LastLoginTime}, PreviousLogin: {player.PreviousLoginTime}, Loginday: {player.Loginday},staminaItem:{player.StaminaItem}, armorItem{player.ArmorItem}");
    }

    private Playerinfo LoadPlayerinfo(string userId)
    {
        if (PlayerPrefs.HasKey($"Player_{userId}_UserName"))
        {
            string userName = PlayerPrefs.GetString($"Player_{userId}_UserName");
            int staminaItem = PlayerPrefs.GetInt($"Player_{userId}_StaminaItem");
            int armorItem = PlayerPrefs.GetInt($"Player_{userId}_ArmorItem");

            // DateTime の読み込み時にデフォルト値を使用
            string lastLoginTimeStr = PlayerPrefs.GetString($"Player_{userId}_LastLoginTime", DateTime.MinValue.ToString("o"));
            string previousLoginTimeStr = PlayerPrefs.GetString($"Player_{userId}_PreviousLoginTime", DateTime.MinValue.ToString("o"));

            DateTime lastLoginTime;
            DateTime previousLoginTime;

            // パースが失敗する場合には MinValue を使用
            if (!DateTime.TryParse(lastLoginTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out lastLoginTime))
            {
                lastLoginTime = DateTime.MinValue;
            }
            if (!DateTime.TryParse(previousLoginTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out previousLoginTime))
            {
                previousLoginTime = DateTime.MinValue;
            }

            int loginday = PlayerPrefs.GetInt($"Player_{userId}_Loginday");

            Debug.Log($"[LOAD] プレイヤー情報を読み込みました: ID: {userId}, Name: {userName}, LastLogin: {lastLoginTime}, PreviousLogin: {previousLoginTime}, Loginday: {loginday}");

            return new Playerinfo(userId, userName)
            {
                StaminaItem = staminaItem,
                ArmorItem = armorItem,
                LastLoginTime = lastLoginTime,
                PreviousLoginTime = previousLoginTime,
                Loginday = loginday
            };
        }

        Debug.Log($"[LOAD] プレイヤーデータが見つかりません: ID = {userId}");
        return null;
    }


    public void ResetPlayerinfo(string userId)
    {
        PlayerPrefs.DeleteKey($"Player_{userId}_UserName");
        PlayerPrefs.DeleteKey($"Player_{userId}_StaminaItem");
        PlayerPrefs.DeleteKey($"Player_{userId}_ArmorItem");
        PlayerPrefs.DeleteKey($"Player_{userId}_LastLoginTime");
        PlayerPrefs.DeleteKey($"Player_{userId}_Loginday");
        PlayerPrefs.DeleteKey($"Player_{userId}_PreviousLoginTime"); // キーを変更
        PlayerPrefs.Save();

        Debug.Log($"Playerinfo をリセットしました: ID = {userId}");
        // リセット後にプレイヤーデータを再ロード
        currentPlayer = LoadPlayerinfo(userId) ?? CreatePlayer(userId, "DefaultName"); // 必要に応じてデフォルト名
        Debug.Log($"Playerinfo をリセット後に再初期化: ID = {currentPlayer.UserId}, Name = {currentPlayer.UserName}");
    }
}

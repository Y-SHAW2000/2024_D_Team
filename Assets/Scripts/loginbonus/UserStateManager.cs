using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UserStateManager : MonoBehaviour
{
    [Serializable]
    public class Playerinfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int StaminaItem { get; set; }
        public int ArmorItem { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int Loginday { get; set; }

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

    private Dictionary<string, Playerinfo> userDatabase = new Dictionary<string, Playerinfo>();
    private Playerinfo currentPlayer;

    // 外部クラスから現在のプレイヤー情報を取得可能にするプロパティ
    public Playerinfo CurrentPlayer => currentPlayer;

    void Start()
    {
        string userId = PhotonNetwork.AuthValues.UserId;
        string userName = PhotonNetwork.NickName;

        currentPlayer = CreatePlayer(userId, userName);

        Debug.Log($"UserStateManager: プレイヤー情報が設定されました - ID: {userId}, Name: {userName}");
    }

    private Playerinfo CreatePlayer(string userId, string userName)
    {
        if (!userDatabase.ContainsKey(userId))
        {
            var newPlayer = new Playerinfo(userId, userName);
            userDatabase[userId] = newPlayer;
            Debug.Log($"新しいプレイヤーを作成しました: ID = {userId}, Name = {userName}");
        }

        return userDatabase[userId];
    }


}

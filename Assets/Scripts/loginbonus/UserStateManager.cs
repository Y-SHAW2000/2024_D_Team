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
            LastLoginTime = DateTime.MinValue; // �����l
            Loginday = 0;
        }
    }

    private Dictionary<string, Playerinfo> userDatabase = new Dictionary<string, Playerinfo>();
    private Playerinfo currentPlayer;

    // �O���N���X���猻�݂̃v���C���[�����擾�\�ɂ���v���p�e�B
    public Playerinfo CurrentPlayer => currentPlayer;

    void Start()
    {
        string userId = PhotonNetwork.AuthValues.UserId;
        string userName = PhotonNetwork.NickName;

        currentPlayer = CreatePlayer(userId, userName);

        Debug.Log($"UserStateManager: �v���C���[��񂪐ݒ肳��܂��� - ID: {userId}, Name: {userName}");
    }

    private Playerinfo CreatePlayer(string userId, string userName)
    {
        if (!userDatabase.ContainsKey(userId))
        {
            var newPlayer = new Playerinfo(userId, userName);
            userDatabase[userId] = newPlayer;
            Debug.Log($"�V�����v���C���[���쐬���܂���: ID = {userId}, Name = {userName}");
        }

        return userDatabase[userId];
    }


}

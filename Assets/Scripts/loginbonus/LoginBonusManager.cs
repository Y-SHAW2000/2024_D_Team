using System;
using UnityEngine;

public class LoginBonusManager : MonoBehaviour
{
    private UserStateManager userStateManager;
    public DateTime LastLoginTime { get; private set; }
    public DateTime PreviousLoginTime { get; private set; }

    void Start()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager が見つかりません！");
            return;
        }
        Debug.Log("UserStateManager を取得しました。");
    }

    public bool IsNewLogin()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        if (playerinfo == null)
        {
            Debug.LogError("プレイヤー情報が設定されていません！");
            return false;
        }

        // 日本時間の現在の日付を取得
        DateTime currentJapanDate = DateTime.UtcNow.AddHours(9).Date;

        // 今日の日付と異なる場合、新しいログイン
        if (playerinfo.LastLoginTime.Date != currentJapanDate)
        {
            Debug.Log($"新しいログインです: 前回ログイン {playerinfo.LastLoginTime}, 今日 {currentJapanDate}");
            playerinfo.PreviousLoginTime = playerinfo.LastLoginTime; //前回のログインを記録
            playerinfo.LastLoginTime = currentJapanDate; // 最終ログインを更新

            Debug.Log("最終ログイン日を更新" + playerinfo.LastLoginTime);

            // ログイン日数を更新
            playerinfo.Loginday = (playerinfo.Loginday < 7) ? playerinfo.Loginday + 1 : 1;

            // 更新後のプレイヤーデータを保存
            userStateManager.SavePlayerinfo(playerinfo);

            LastLoginTime = currentJapanDate; // 最終ログインを更新
            UseItemManager.GetLogInBonus();
            return true;
        }
        else
        {
            Debug.Log($"本日は既にログイン済みです: 前回ログイン {playerinfo.LastLoginTime}, 今日 {currentJapanDate}");
            return false;
        }
    }
    public void UpdateLoginData()
    {
        var playerinfo = userStateManager.CurrentPlayer; // 現在のプレイヤー情報を取得
        if (playerinfo == null)
        {
            Debug.LogError("プレイヤー情報が設定されていません！");
            return;
        }

        // 日本時間の現在の日付を取得
        DateTime currentJapanDate = DateTime.UtcNow.AddHours(9).Date;

        // 最終ログイン時間を更新
        playerinfo.PreviousLoginTime = playerinfo.LastLoginTime;
        playerinfo.LastLoginTime = currentJapanDate;

        // 更新後のプレイヤーデータを保存
        userStateManager.SavePlayerinfo(playerinfo);

        Debug.Log($"ログインデータが更新されました: {currentJapanDate}");
    }


}

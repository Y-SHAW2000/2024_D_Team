using System;
using UnityEngine;

public class LoginBonusManager : MonoBehaviour
{
    public DateTime ResetTime => DateTime.UtcNow.AddHours(9); // 日本の現在時刻
    private string lastLoginKey = "LastLoginDate";
    private string currentDayKey = "CurrentLoginDay";

    public bool IsNewLogin()
    {
        // 最終ログイン日を取得
        string lastLoginDate = PlayerPrefs.GetString(lastLoginKey, "");
        DateTime lastLogin;

        if (string.IsNullOrEmpty(lastLoginDate))
        {
            // 初回ログイン時は現在の日付を設定
            lastLogin = ResetTime.Date; // 日付
            PlayerPrefs.SetString(lastLoginKey, lastLogin.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
        else
        {
            // 保存されている日付を解析
            lastLogin = DateTime.Parse(lastLoginDate);
        }

        // 現在の時刻と比較して、新しいログインかを判定
        return lastLogin.Date != ResetTime.Date;
    }

    public int GetCurrentDay()
    {
        return PlayerPrefs.GetInt(currentDayKey, 1);
    }

    public void UpdateLoginData()
    {
        int currentDay = GetCurrentDay();

        // Day7まで到達していればDay1に戻す
        if (currentDay >= 7)
        {
            currentDay = 1;
        }
        else
        {
            currentDay++;
        }

        // ログイン情報を保存
        PlayerPrefs.SetString(lastLoginKey, ResetTime.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt(currentDayKey, currentDay);
        PlayerPrefs.Save();
    }
}

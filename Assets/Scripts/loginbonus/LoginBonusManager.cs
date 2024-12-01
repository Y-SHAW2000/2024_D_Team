using UnityEngine;
using System;

public class LoginBonusManager : MonoBehaviour
{
    private string LastLogin;
    private string CurrentTime;

    void Start()
    {
        // 日本時間（UTC+9）を取得
        DateTime japanTime = DateTime.UtcNow.AddHours(9);

        // 日本時間をフォーマットしてログに出力
        Debug.Log($"日本時間: {japanTime.ToString("yyyy-MM-dd HH:mm:ss")}");
    }

    public void IsNewLogin()
    {
        if (LastLogin == null)
        {
            // LastLogin が null の場合、日本の現在時刻をフォーマットして保存
            DateTime japanTime = DateTime.UtcNow.AddHours(9);
            LastLogin = japanTime.ToString("yyyy-MM-dd HH:mm:ss");

            Debug.Log($"初回ログインとして記録: {LastLogin}");
        }
        else
        {
            Debug.Log($"既にログイン日時が記録されています: {LastLogin}");
        }
    }
}

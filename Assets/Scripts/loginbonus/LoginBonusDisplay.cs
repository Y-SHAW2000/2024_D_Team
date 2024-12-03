using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginBonusDisplay : MonoBehaviour
{
    private UserStateManager userStateManager;

    // 各ログインボーナス日を表すテキスト
    public List<Text> dayTexts;

    IEnumerator Start()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager が見つかりません！");
            yield break;
        }

        while (userStateManager.CurrentPlayer == null)
        {
            Debug.Log("LoginBonusDisplay: CurrentPlayer が設定されるのを待機中...");
            yield return null;
        }

        Debug.Log("LoginBonusDisplay: CurrentPlayer を認識しました！");
        DisplayLoginBonusStatus();
    }


    private void DisplayLoginBonusStatus()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        if (playerinfo == null)
        {
            Debug.LogError("プレイヤー情報が設定されていません！");
            return;
        }

        Debug.Log("playerinfoは取得されました");
        int loginday = playerinfo.Loginday;

        // すべてのテキストを非表示またはリセット
        foreach (var text in dayTexts)
        {
            text.gameObject.SetActive(false); // 非表示にする
        }

        // Loginday 分だけ「済」を設定して表示
        for (int i = 0; i < loginday; i++)
        {
            if (i < dayTexts.Count)
            {
                dayTexts[i].gameObject.SetActive(true); // 表示
                Debug.Log($"Day {i + 1} に「済」を表示しました。");
            }
        }
    }
}

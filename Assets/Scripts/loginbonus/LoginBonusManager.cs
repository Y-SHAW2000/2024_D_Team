using System;
using UnityEngine;

public class LoginBonusManager : MonoBehaviour
{
    public DateTime ResetTime => DateTime.UtcNow.AddHours(9); // ���{�̌��ݎ���
    private string lastLoginKey = "LastLoginDate";
    private string currentDayKey = "CurrentLoginDay";

    public bool IsNewLogin()
    {
        // �ŏI���O�C�������擾
        string lastLoginDate = PlayerPrefs.GetString(lastLoginKey, "");
        DateTime lastLogin;

        if (string.IsNullOrEmpty(lastLoginDate))
        {
            // ���񃍃O�C�����͌��݂̓��t��ݒ�
            lastLogin = ResetTime.Date; // ���t
            PlayerPrefs.SetString(lastLoginKey, lastLogin.ToString("yyyy-MM-dd"));
            PlayerPrefs.Save();
        }
        else
        {
            // �ۑ�����Ă�����t�����
            lastLogin = DateTime.Parse(lastLoginDate);
        }

        // ���݂̎����Ɣ�r���āA�V�������O�C�����𔻒�
        return lastLogin.Date != ResetTime.Date;
    }

    public int GetCurrentDay()
    {
        return PlayerPrefs.GetInt(currentDayKey, 1);
    }

    public void UpdateLoginData()
    {
        int currentDay = GetCurrentDay();

        // Day7�܂œ��B���Ă����Day1�ɖ߂�
        if (currentDay >= 7)
        {
            currentDay = 1;
        }
        else
        {
            currentDay++;
        }

        // ���O�C������ۑ�
        PlayerPrefs.SetString(lastLoginKey, ResetTime.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt(currentDayKey, currentDay);
        PlayerPrefs.Save();
    }
}

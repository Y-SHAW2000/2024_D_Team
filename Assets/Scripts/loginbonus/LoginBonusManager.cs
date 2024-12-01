using System;

using UnityEngine;

public class LoginBonusManager : MonoBehaviour
{
    private UserStateManager userStateManager;

    public DateTime LastLoginTime { get; private set; }

    void Start()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager ��������܂���I");
            return;
        }

        Debug.Log("UserStateManager ���擾���܂����B");
    }

    public bool IsNewLogin()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        if (playerinfo == null)
        {
            Debug.LogError("�v���C���[��񂪐ݒ肳��Ă��܂���I");
            return false;
        }

        // ���{���Ԃ̌��݂̓��t���擾
        DateTime currentJapanDate = DateTime.UtcNow.AddHours(9).Date;

        // �����̓��t�ƈقȂ�ꍇ�A�V�������O�C��
        if (playerinfo.LastLoginTime.Date != currentJapanDate)
        {
            Debug.Log($"�V�������O�C���ł�: �O�񃍃O�C�� {playerinfo.LastLoginTime}, ���� {currentJapanDate}");
            playerinfo.LastLoginTime = currentJapanDate; // �ŏI���O�C�����X�V
            Debug.Log("�ŏI���O�C�������X�V" + playerinfo.LastLoginTime);

            // ���O�C���������X�V
            playerinfo.Loginday = (playerinfo.Loginday < 7) ? playerinfo.Loginday + 1 : 1;

            // �X�V��̃v���C���[�f�[�^��ۑ�
            userStateManager.SavePlayerinfo(playerinfo);

            LastLoginTime = currentJapanDate; // �ŏI���O�C�����X�V
            return true;
        }

        Debug.Log($"�{���͊��Ƀ��O�C���ς݂ł�: �O�񃍃O�C�� {playerinfo.LastLoginTime}, ���� {currentJapanDate}");
        return false;
    }
    public void UpdateLoginData()
    {
        var playerinfo = userStateManager.CurrentPlayer; // ���݂̃v���C���[�����擾
        if (playerinfo == null)
        {
            Debug.LogError("�v���C���[��񂪐ݒ肳��Ă��܂���I");
            return;
        }

        // ���{���Ԃ̌��݂̓��t���擾
        DateTime currentJapanDate = DateTime.UtcNow.AddHours(9).Date;

        // �ŏI���O�C�����Ԃ��X�V
        playerinfo.LastLoginTime = currentJapanDate;

        // �X�V��̃v���C���[�f�[�^��ۑ�
        userStateManager.SavePlayerinfo(playerinfo);

        Debug.Log($"���O�C���f�[�^���X�V����܂���: {currentJapanDate}");
    }

}

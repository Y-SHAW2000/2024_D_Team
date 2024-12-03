using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginBonusDisplay : MonoBehaviour
{
    private UserStateManager userStateManager;

    // �e���O�C���{�[�i�X����\���e�L�X�g
    public List<Text> dayTexts;

    IEnumerator Start()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager ��������܂���I");
            yield break;
        }

        while (userStateManager.CurrentPlayer == null)
        {
            Debug.Log("LoginBonusDisplay: CurrentPlayer ���ݒ肳���̂�ҋ@��...");
            yield return null;
        }

        Debug.Log("LoginBonusDisplay: CurrentPlayer ��F�����܂����I");
        DisplayLoginBonusStatus();
    }


    private void DisplayLoginBonusStatus()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        if (playerinfo == null)
        {
            Debug.LogError("�v���C���[��񂪐ݒ肳��Ă��܂���I");
            return;
        }

        Debug.Log("playerinfo�͎擾����܂���");
        int loginday = playerinfo.Loginday;

        // ���ׂẴe�L�X�g���\���܂��̓��Z�b�g
        foreach (var text in dayTexts)
        {
            text.gameObject.SetActive(false); // ��\���ɂ���
        }

        // Loginday �������u�ρv��ݒ肵�ĕ\��
        for (int i = 0; i < loginday; i++)
        {
            if (i < dayTexts.Count)
            {
                dayTexts[i].gameObject.SetActive(true); // �\��
                Debug.Log($"Day {i + 1} �Ɂu�ρv��\�����܂����B");
            }
        }
    }
}

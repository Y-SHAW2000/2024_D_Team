using UnityEngine;
using System;

public class LoginBonusManager : MonoBehaviour
{
    private string LastLogin;
    private string CurrentTime;

    void Start()
    {
        // ���{���ԁiUTC+9�j���擾
        DateTime japanTime = DateTime.UtcNow.AddHours(9);

        // ���{���Ԃ��t�H�[�}�b�g���ă��O�ɏo��
        Debug.Log($"���{����: {japanTime.ToString("yyyy-MM-dd HH:mm:ss")}");
    }

    public void IsNewLogin()
    {
        if (LastLogin == null)
        {
            // LastLogin �� null �̏ꍇ�A���{�̌��ݎ������t�H�[�}�b�g���ĕۑ�
            DateTime japanTime = DateTime.UtcNow.AddHours(9);
            LastLogin = japanTime.ToString("yyyy-MM-dd HH:mm:ss");

            Debug.Log($"���񃍃O�C���Ƃ��ċL�^: {LastLogin}");
        }
        else
        {
            Debug.Log($"���Ƀ��O�C���������L�^����Ă��܂�: {LastLogin}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public LoginBonusManager loginBonusManager;

    public void SceneStart()
    {
        if (loginBonusManager.IsNewLogin())
        {
            SceneManager.LoadScene("LoginBonusScene"); // ���O�C���{�[�i�X��
        }
        else
        {
            SceneManager.LoadScene("HomeScene"); // �z�[����
        }
    }

    public void ReceiveBonus()
    {
        loginBonusManager.UpdateLoginData();
        SceneManager.LoadScene("HomeScene"); // �z�[����ʂ�
    }
}


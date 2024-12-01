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
            SceneManager.LoadScene(Scenenames.LoginBonusScene);// ���O�C���{�[�i�X��
        }
        else
        {
            SceneManager.LoadScene(Scenenames.HomeScene); // �z�[����
        }
    }

    public void ReceiveBonus()
    {
        loginBonusManager.UpdateLoginData();
        SceneManager.LoadScene(Scenenames.HomeScene);// �z�[����ʂ�
    }
}


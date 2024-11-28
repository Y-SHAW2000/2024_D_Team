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
            SceneManager.LoadScene("LoginBonusScene"); // ログインボーナスへ
        }
        else
        {
            SceneManager.LoadScene("HomeScene"); // ホームへ
        }
    }

    public void ReceiveBonus()
    {
        loginBonusManager.UpdateLoginData();
        SceneManager.LoadScene("HomeScene"); // ホーム画面へ
    }
}


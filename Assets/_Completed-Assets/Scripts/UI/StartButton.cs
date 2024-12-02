using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{

    private LoginBonusManager loginBonusManager;

    
    [SerializeField]
    private Button startButton;
    // Start is called before the first frame update
    void Start()
    {

        startButton.onClick.AddListener(OnClicked);

        // LoginBonusManager を取得
        loginBonusManager = FindObjectOfType<LoginBonusManager>();
        if (loginBonusManager == null)
        {
            Debug.LogError("LoginBonusManager が見つかりません！");
            return;
        }
    }

    private void OnClicked()
    {
        //SceneManager.LoadScene(Scenenames.HomeScene);

        if (loginBonusManager.IsNewLogin())
        {
            SceneManager.LoadScene("LoginBonusScene");// ログインボーナスへ
            loginBonusManager.UpdateLoginData();
            Debug.Log("ログインボーナス画面に行く");
        }
        else
        {
            SceneManager.LoadScene("HomeScene"); // ホームへ
            Debug.Log("ホームへいく");
        }
    }

}

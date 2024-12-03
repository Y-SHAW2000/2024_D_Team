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

        // LoginBonusManager ���擾
        loginBonusManager = FindObjectOfType<LoginBonusManager>();
        if (loginBonusManager == null)
        {
            Debug.LogError("LoginBonusManager ��������܂���I");
            return;
        }
    }

    private void OnClicked()
    {
        //SceneManager.LoadScene(Scenenames.HomeScene);

        if (loginBonusManager.IsNewLogin())
        {
            SceneManager.LoadScene("LoginBonusScene");// ���O�C���{�[�i�X��
            loginBonusManager.UpdateLoginData();
            Debug.Log("���O�C���{�[�i�X��ʂɍs��");
        }
        else
        {
            SceneManager.LoadScene("HomeScene"); // �z�[����
            Debug.Log("�z�[���ւ���");
        }
    }

}

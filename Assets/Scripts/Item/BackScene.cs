using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackScene : MonoBehaviour
{
    [SerializeField]
    private Button BackSceneButton;
    // Start is called before the first frame update

    void Start()
    {
        BackSceneButton.onClick.AddListener(OnClicked);

    }

    private void OnClicked()
    {

        SceneManager.LoadScene("HomeScene");

    }
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Logintohome : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    // Start is called before the first frame update
    void Start()
    {

        startButton.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        SceneManager.LoadScene("HomeScene");
    }

}

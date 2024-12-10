using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerVersusButton : MonoBehaviour
{   
    [SerializeField]
    private Button playerVersusButton;
    // Start is called before the first frame update
    void Start()
    {
    playerVersusButton.onClick.AddListener(OnClicked);
        
    }

    private void OnClicked()
    {
        
        SceneManager.LoadScene("LobbyScene");

    }
    void Update()
    {
        
    }
}

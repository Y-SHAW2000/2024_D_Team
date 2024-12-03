using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoLobby : MonoBehaviour
{
    [SerializeField]
    private Button goLobbyButton;
    // Start is called before the first frame update
    void Start()
    {
        goLobbyButton.onClick.AddListener(OnClicked);
    }
     private void OnClicked()
    {
        SceneManager.LoadScene("LobbyScene");

    }
}

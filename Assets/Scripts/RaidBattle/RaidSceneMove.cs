using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RaidSceneMove : MonoBehaviour
{

    private UserStateManager userstate;
    [SerializeField]
    private Button ItemSceneButton;
    // Start is called before the first frame update

    void Awake()
    {
        userstate = FindObjectOfType<UserStateManager>();
    }
    void Start()
    {
        ItemSceneButton.onClick.AddListener(OnClicked);

    }

    private void OnClicked()
    {
        UseStamina();
        SceneManager.LoadScene("RaidBattle");

    }
    private void UseStamina()
    {
        var playerinfo = userstate.CurrentPlayer;
        playerinfo.Stamina -= 1;
        Debug.Log("スタミナを1消費した");
        userstate.SavePlayerinfo(playerinfo);
    }
}

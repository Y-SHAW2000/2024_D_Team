using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoHomeScene : MonoBehaviour
{
    [SerializeField]
    private Button goHomeButton;
    // Start is called before the first frame update
    void Start()
    {
        goHomeButton.onClick.AddListener(OnClicked);
    }
     private void OnClicked()
    {
        SceneManager.LoadScene(Scenenames.HomeScene);

    }
}

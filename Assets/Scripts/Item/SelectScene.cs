using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectScene : MonoBehaviour
{
    [SerializeField]
    private Button ItemSceneButton;
    // Start is called before the first frame update

    void Start()
    {
        ItemSceneButton.onClick.AddListener(OnClicked);

    }

    private void OnClicked()
    {

        SceneManager.LoadScene("ItemScene");

    }
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    private Button openButton;

    public GameObject openTarget;

    private void Start()
    {
        openButton = GetComponent<Button>();
        openButton.onClick.AddListener(CloseUI);
    }

    private void CloseUI()
    {
        openTarget.SetActive(true);
    }
}

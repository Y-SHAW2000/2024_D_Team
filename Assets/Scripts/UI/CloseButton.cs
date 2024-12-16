using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private Button closeButton;

    public GameObject closeTarget;

    private void Start()
    {
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(CloseUI);
    }

    private void CloseUI()
    {
        closeTarget.SetActive(false);
    }
}

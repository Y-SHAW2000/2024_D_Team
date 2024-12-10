using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailEntry : MonoBehaviour
{
    public GameObject mailContentWindow;
    public void onClick()
    {
        var titleText = mailContentWindow.transform.Find("Title").GetComponent<TMP_Text>();
        var contentText = mailContentWindow.transform.Find("ContentText").GetComponent<TMP_Text>();

        titleText.text = gameObject.transform.Find("TitileText").GetComponent<TMP_Text>().text;
        contentText.text = gameObject.transform.Find("ContentText").GetComponent<TMP_Text>().text;
        mailContentWindow.SetActive(true);
    }
}

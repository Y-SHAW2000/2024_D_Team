using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UserIDUIManager : MonoBehaviour
{
    private TMP_Text userIDText;
    private TMP_Text userNameText;

    public GameObject changeNameUI;

    private void Start()
    {
        GetUserText();
        UpdateUserInfoDisplay();
        
    }

    private void GetUserText()
    {
        userIDText = GameObject.Find("Canvas/UserIDText").GetComponent<TMP_Text>();
        userNameText = GameObject.Find("Canvas/UserNameText").GetComponent<TMP_Text>();
    }

    private void UpdateUserInfoDisplay()
    {
        if (PhotonNetwork.AuthValues.UserId != null)
        {
            userNameText.text = "UserName: " + PhotonNetwork.NickName;
            userIDText.text = "UserID: " + PhotonNetwork.AuthValues.UserId;
        }
        else
        {
            userNameText.text = "First Login";
            userIDText.text = "";
        }
    }

    public void OpenChangeNameUI()
    {
        changeNameUI.SetActive(true);
        changeNameUI.GetComponentInChildren<TMP_InputField>().text = PhotonNetwork.NickName;
    }

    public void CloseChangeNameUI()
    {
        changeNameUI.SetActive(false);
    }

    public void ChangeName()
    {
        string newUserName = changeNameUI.GetComponentInChildren<TMP_InputField>().text;
        if (IsValidUserName(newUserName))
        {
            PhotonNetwork.NickName = newUserName;
            PlayerPrefs.SetString("UserName", newUserName);
            UpdateUserInfoDisplay();
            Debug.Log("UserName: " + PhotonNetwork.NickName);
        }
        else
        {
            Debug.LogError("Invalid username!");
        }
        changeNameUI.SetActive(false);
    }

    private bool IsValidUserName(string name)
    {
        if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 15)
            return false;

        foreach (char c in name)
        {
            if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                return false;
        }

        return true;
    }
}

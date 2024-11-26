using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UserManager : MonoBehaviourPunCallbacks
{
    public string defaultUserName = "NoName";

    private void Awake()
    {
        string userId = PlayerPrefs.GetString("UserId", System.Guid.NewGuid().ToString());
        PlayerPrefs.SetString("UserId", userId);

        PhotonNetwork.AuthValues = new AuthenticationValues(userId);
        string userName = PlayerPrefs.GetString("UserName", defaultUserName);
        PhotonNetwork.NickName = userName;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon. UserId: " + PhotonNetwork.AuthValues.UserId);
        Debug.Log("UserName: " + PhotonNetwork.NickName);
    }
}

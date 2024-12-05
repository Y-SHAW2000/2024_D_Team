using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public Button readyButton;
    public Button leaveButton;
    public GameObject[] stampButtons; // 6種類のスタンプボタン
    public Image playerStampDisplay;
    public Image opponentStampDisplay;
    public TMP_Text readyStatusText;

    private bool isReady = false;

    private void Start()
    {
        // ルームオプションを設定
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2 // ルームの最大人数を設定
        };

        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, TypedLobby.Default);
        leaveButton.onClick.AddListener(OnLeaveLobby);
        readyButton.onClick.AddListener(OnReadyPressed);

        foreach (var stampButton in stampButtons)
        {
            stampButton.GetComponent<Button>().onClick.AddListener(() => SendStamp(stampButton.name));
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} joined the room.");
    }

    private void OnLeaveLobby()
    {
        if (PhotonNetwork.InRoom)
        {
            // ルームに参加している場合はLeaveRoomを呼び出し
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            // ルームに参加していない場合は直接シーン遷移
            Debug.LogWarning("Not in a room, returning to HomeScene directly.");
            SceneManager.LoadScene("HomeScene");
        }
    }

    private void OnReadyPressed()
    {
        isReady = !isReady;

        // 自分の Ready 状態をルームのカスタムプロパティに保存
        Hashtable playerProperties = new Hashtable
        {
            { "IsReady", isReady }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

        // 自分の Ready 状態を更新
        UpdateReadyStatus();

        // Ready 状態の確認
        CheckReadyStatus();
    }

    private void CheckReadyStatus()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue("IsReady", out object isPlayerReady))
            {
                if ((bool)isPlayerReady)
                {
                    Debug.Log("At least one player is ready! Moving to battle scene.");
                    SceneManager.LoadScene("_Complete-Game");
                    return;
                }
            }
        }

        Debug.Log("No players are ready.");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("IsReady"))
        {
            Debug.Log($"Player {targetPlayer.NickName} Ready State Updated: {changedProps["IsReady"]}");
            CheckReadyStatus();
        }
    }

    private void UpdateReadyStatus()
    {
        readyStatusText.text = isReady ? "You are Ready!" : "You are Not Ready";
    }

    private void SendStamp(string stampName)
    {
        photonView.RPC("ReceiveStamp", RpcTarget.Others, stampName);
        DisplayStamp(playerStampDisplay, stampName);
    }

    [PunRPC]
    private void ReceiveStamp(string stampName)
    {
        DisplayStamp(opponentStampDisplay, stampName);
    }

    private void DisplayStamp(Image stampDisplay, string stampName)
    {
        // スタンプを表示し、5秒後にフェードアウトするロジック
        Sprite stampSprite = Resources.Load<Sprite>($"Stamps/{stampName}"); // スタンプ画像リソースをロード

        stampDisplay.sprite = stampSprite;
        stampDisplay.color = Color.white;
        stampDisplay.SetNativeSize();
        CancelInvoke(nameof(HideStamp));
        Invoke(nameof(HideStamp), 5f);
    }

    private IEnumerator FadeOutStamp(Image stampDisplay)
    {
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            Color color = stampDisplay.color;
            color.a = alpha;
            stampDisplay.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void HideStamp()
    {
        StartCoroutine(FadeOutStamp(playerStampDisplay));
    }
}

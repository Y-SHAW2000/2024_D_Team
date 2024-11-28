using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        PhotonNetwork.ConnectUsingSettings();
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
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
        photonView.RPC("NotifyReady", RpcTarget.Others, isReady);
        UpdateReadyStatus();
        // 自分が準備完了した場合、相手が準備完了かチェックして対戦画面に遷移
        if (isReady && PhotonNetwork.IsMasterClient)
        {
            CheckIfBothReady();
        }
    }

    [PunRPC]
    private void NotifyReady(bool opponentReady)
    {
        readyStatusText.text = opponentReady ? "Opponent is Ready!" : "Opponent is Not Ready";
        // もし両方のプレイヤーが準備完了なら、ネットワーク対戦画面に遷移
        if (isReady && opponentReady && PhotonNetwork.IsMasterClient)
        {
            StartMatch();
        }
    }
    private void StartMatch()
    {
        // ここで対戦画面に遷移する
        SceneManager.LoadScene("_Complete-Game");
    }
    private void CheckIfBothReady()
    {
        // 現在の準備状態が相手と一致するか確認
        photonView.RPC("CheckReadyStatus", RpcTarget.Others, isReady);
    }
    [PunRPC]
    private void CheckReadyStatus(bool playerReady)
    {
        if (playerReady && isReady)
        {
            StartMatch();
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
    private void HideStamp()
    {
        playerStampDisplay.color = new Color(1, 1, 1, 0); // フェードアウト
    }
}
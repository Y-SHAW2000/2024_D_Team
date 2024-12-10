using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteButton : MonoBehaviour
{
    private UserStateManager userStateManager;

    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnClicked);

        // LoginBonusManager を取得
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("userStateManager が見つかりません！");
            return;
        }
    }

    private void OnClicked()
    {
        // ユーザーデータをリセットする
        if (userStateManager.CurrentPlayer != null)
        {
            userStateManager.ResetPlayerinfo(userStateManager.CurrentPlayer.UserId);
            Debug.Log("ユーザーデータをリセットしました。");
        }
    }
}

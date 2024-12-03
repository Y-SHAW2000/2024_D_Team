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

        // LoginBonusManager ���擾
        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("userStateManager ��������܂���I");
            return;
        }
    }

    private void OnClicked()
    {
        // ���[�U�[�f�[�^�����Z�b�g����
        if (userStateManager.CurrentPlayer != null)
        {
            userStateManager.ResetPlayerinfo(userStateManager.CurrentPlayer.UserId);
            Debug.Log("���[�U�[�f�[�^�����Z�b�g���܂����B");
        }
    }
}

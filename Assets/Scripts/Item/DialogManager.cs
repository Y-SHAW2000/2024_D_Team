using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text dialogText;
    public Button closeButton;

    private void Start()
    {
        dialogPanel.SetActive(false);
        GetItem.OnButtonClicked += ShowDialog;
        closeButton.onClick.AddListener(CloseDialog);
    }

    public void ShowDialog(GameObject ItemButtonGameObject)
    {
        if (ItemButtonGameObject.name == "Defence_Use_Button")
        {
            dialogText.text = $" �g�p���܂���\n\n����\n\n�E�g�p���邱�ƂŁA���̃v���C���݈̂ꎞ�I�Ɏ��@�̍ő�HP��2�{�ƂȂ�B\r\n�E���̌��ʂ��K�p�����̂́A�l�b�g���[�N�ΐ�@�\�ƁA���C�h�o�g���@�\�݂̂Ƃ���B\n�E����������@��HP��0�ƂȂ����^�C�~���O�ōő�HP�͌��̒l�ɖ߂�B\r\n";
            dialogPanel.SetActive(true);
        }
        else if (ItemButtonGameObject.name == "Stamina_Use_Button")
        {
            dialogText.text = $"�g�p���܂���\n\n����\n\n �E�g�p���邱�ƂŃv���C���[�̃X�^�~�i��1�񕜂���B\r\n";
            dialogPanel.SetActive(true);
        }
        else
        {
            dialogText.text = $"\n\n\n�G���[���������܂���";
            dialogPanel.SetActive(true);
        }

    }

    public void CloseDialog()
    {
        // �_�C�A���O���\��
        dialogPanel.SetActive(false);
    }
}
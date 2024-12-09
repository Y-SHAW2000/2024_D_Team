using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private ItemManager itemmanager;
    public GameObject dialogPanel;
    public Text dialogText;
    public Button closeButton;

    private void Awake()
    {
        itemmanager = FindObjectOfType<ItemManager>();
        dialogPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseDialog);
    }

    private void OnDisable()
    {
        // �A�C�e���ύX�C�x���g�̍w�ǂ�����
        GetItem.OnButtonClicked -= ShowDialog;
    }*/

    public void ShowDialog(GameObject ItemButtonGameObject)
    {
        // `dialogPanel` ����\���ł��\�����ł��A���e���X�V
        if (ItemButtonGameObject.name == "Defence_Use_Button")
        {
            if (UseItemManager.useArmor == false)
            {
                dialogText.text = $" �g�p���܂���\n\n����\n\n�E�g�p���邱�ƂŁA���̃v���C���݈̂ꎞ�I�Ɏ��@�̍ő�HP��2�{�ƂȂ�B\r\n�E���̌��ʂ��K�p�����̂́A�l�b�g���[�N�ΐ�@�\�ƁA���C�h�o�g���@�\�݂̂Ƃ���B\n�E����������@��HP��0�ƂȂ����^�C�~���O�ōő�HP�͌��̒l�ɖ߂�B\r\n";
                itemmanager.HandleItemReceived(ItemButtonGameObject);
                dialogPanel.SetActive(true);
            }
            else
            {
                dialogText.text = $"\n\n\n���Ɍ��ʂ��K�p����Ă��܂��B";
                dialogPanel.SetActive(true);
            }
        }
        else if (ItemButtonGameObject.name == "Stamina_Use_Button")
        {
            if(UseItemManager.useStamina == false)
            {
                dialogText.text = $"�g�p���܂���\n\n����\n\n �E�g�p���邱�ƂŃv���C���[�̃X�^�~�i��1�񕜂���B\r\n";
                itemmanager.HandleItemReceived(ItemButtonGameObject);
                dialogPanel.SetActive(true);
            }
            else
            {
                dialogText.text = $"\n\n\n���Ɍ��ʂ��K�p����Ă��܂��B";
                dialogPanel.SetActive(true);
            }
        }
        else
        {
            dialogText.text = $"\n\n\n�G���[���������܂���";
        }

    }

    public void CloseDialog()
    {
        // �_�C�A���O���\��
        dialogPanel.SetActive(false);
    }
}

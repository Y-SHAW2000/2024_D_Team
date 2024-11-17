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
            dialogText.text = $" 使用しました\n\n効果\n\n・使用することで、次のプレイ時のみ一時的に自機の最大HPが2倍となる。\r\n・この効果が適用されるのは、ネットワーク対戦機能と、レイドバトル機能のみとする。\n・いずれも自機のHPが0となったタイミングで最大HPは元の値に戻る。\r\n";
            dialogPanel.SetActive(true);
        }
        else if (ItemButtonGameObject.name == "Stamina_Use_Button")
        {
            dialogText.text = $"使用しました\n\n効果\n\n ・使用することでプレイヤーのスタミナを1回復する。\r\n";
            dialogPanel.SetActive(true);
        }
        else
        {
            dialogText.text = $"\n\n\nエラーが発生しました";
            dialogPanel.SetActive(true);
        }
    }

    public void CloseDialog()
    {
        // ダイアログを非表示
        dialogPanel.SetActive(false);
    }
}

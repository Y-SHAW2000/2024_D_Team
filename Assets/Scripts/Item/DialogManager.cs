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
        // アイテム変更イベントの購読を解除
        GetItem.OnButtonClicked -= ShowDialog;
    }*/

    public void ShowDialog(GameObject ItemButtonGameObject)
    {
        // `dialogPanel` が非表示でも表示中でも、内容を更新
        if (ItemButtonGameObject.name == "Defence_Use_Button")
        {
            if (UseItemManager.useArmor == false)
            {
                dialogText.text = $" 使用しました\n\n効果\n\n・使用することで、次のプレイ時のみ一時的に自機の最大HPが2倍となる。\r\n・この効果が適用されるのは、ネットワーク対戦機能と、レイドバトル機能のみとする。\n・いずれも自機のHPが0となったタイミングで最大HPは元の値に戻る。\r\n";
                itemmanager.HandleItemReceived(ItemButtonGameObject);
                dialogPanel.SetActive(true);
            }
            else
            {
                dialogText.text = $"\n\n\n既に効果が適用されています。";
                dialogPanel.SetActive(true);
            }
        }
        else if (ItemButtonGameObject.name == "Stamina_Use_Button")
        {
            if(UseItemManager.useStamina == false)
            {
                dialogText.text = $"使用しました\n\n効果\n\n ・使用することでプレイヤーのスタミナを1回復する。\r\n";
                itemmanager.HandleItemReceived(ItemButtonGameObject);
                dialogPanel.SetActive(true);
            }
            else
            {
                dialogText.text = $"\n\n\n既に効果が適用されています。";
                dialogPanel.SetActive(true);
            }
        }
        else
        {
            dialogText.text = $"\n\n\nエラーが発生しました";
        }

    }

    public void CloseDialog()
    {
        // ダイアログを非表示
        dialogPanel.SetActive(false);
    }
}

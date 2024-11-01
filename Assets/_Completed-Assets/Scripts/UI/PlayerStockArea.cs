using UnityEngine;
using UnityEngine.UI;
using Complete;

public class PlayerStockArea : MonoBehaviour
{
    // 弾の参照 (Shell1〜Shell10)
    [SerializeField] private Image[] SingleBullet;

    // 10発分の弾の参照 (Shells10〜Shells40)
    [SerializeField] private Image[] Bullets;

    // ストック数に応じて砲弾アイコンを表示する
    public void UpdatePlayerStockArea(int StockCount)
    {
        // 1発アイコンの表示・非表示設定
        for (int i = 0; i < SingleBullet.Length; i++)
        {
            // stockCountに応じて該当アイコンを表示または非表示
            SingleBullet[i].gameObject.SetActive(i < StockCount % 10); //()内を満たす分表示する
        }

        // 10発アイコンの表示・非表示設定
        int TenCount = StockCount / 10;
        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].gameObject.SetActive(i < TenCount);
        }
    }
}


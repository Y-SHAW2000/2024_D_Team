using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Complete;
using Photon.Pun;


public class PlayerStockArea : MonoBehaviourPun
{
    // �e�̎Q�� (Shell1�`Shell10)
    [SerializeField] private Image[] SingleBullet;

    // 10�����̒e�̎Q�� (Shells10�`Shells40)
    [SerializeField] private Image[] Bullets;

    [SerializeField] private Image[] Mines;

    private WeaponStockData weaponstockdata;
    private void Start()
    {
        {
            // weaponstockdata が null の場合にインスタンス化
            if (weaponstockdata == null)
            {
                weaponstockdata = new WeaponStockData(); // インスタンスを生成
            }

            // weaponstockdata.weaponInitial が null でないことを確認
            if (weaponstockdata != null && weaponstockdata.weaponInitial != null)
            {
                // weaponInitial が初期化されている場合、更新
                UpdatePlayerStockArea(weaponstockdata.weaponInitial);
            }
            else
            {
                // weaponInitial が null の場合、デフォルトの初期値を設定
                Dictionary<string, int> defaultWeaponStock = new Dictionary<string, int>
                {
                    { "Shell", 20 },  // 初期シェルの数
                    { "Mine", 5 }     // 初期マインの数
                };

                UpdatePlayerStockArea(defaultWeaponStock);
            }
        }

    
    }



    // プレイヤーストックエリアを更新
    public void UpdatePlayerStockArea(Dictionary<string, int> weaponStock)
    {
        // PUN2 RPCで更新を通知
        photonView.RPC("SyncWeaponStock", RpcTarget.All, weaponStock);
    }

    // RPCメソッドで、武器在庫の変更を全クライアントに通知
    [PunRPC]
    private void SyncWeaponStock(Dictionary<string, int> weaponStock)
    {
        // シェルの個数に基づいて表示を更新
        for (int i = 0; i < SingleBullet.Length; i++)
        {
            if (weaponStock["Shell"] != 50)
            {
                SingleBullet[i].gameObject.SetActive(i < weaponStock["Shell"] % 10);
            }
            else
            {
                SingleBullet[i].gameObject.SetActive(i < 10);
            }
        }

        // 10個単位のシェル
        int TenCount = weaponStock["Shell"] / 10;
        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].gameObject.SetActive(i < TenCount);
        }

        // マインの表示を更新
        for (int i = 0; i < Mines.Length; i++)
        {
            Mines[i].gameObject.SetActive(i < weaponStock["Mine"]);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;
using Photon.Pun;

namespace Complete
{
    public class HudManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerStockArea player1StockArea; // Player1のHUD
        [SerializeField] private PlayerStockArea player2StockArea; // Player2のHUD
        [SerializeField] private Complete.GameManager gameManager; // GameManager
        [SerializeField] private Complete.TankManager tankManager; // TankManager

        private void Start()
        {
            if (gameManager != null)
            {
                gameManager.OnGameStateChanged += HandleGameStateChanged;
                foreach (var tank in gameManager.m_Tanks)
                {
                    tank.OnWeaponStockChanged += UpdatePlayerStockArea;
                }
            }
        }

        private void OnDestroy()
        {
            if (gameManager != null)
            {
                gameManager.OnGameStateChanged -= HandleGameStateChanged;
            }
        }

        private void HandleGameStateChanged(Complete.GameManager.GameState gameState)
        {
            if (gameState == Complete.GameManager.GameState.RoundPlaying)
            {
                player1StockArea.gameObject.SetActive(true);
                player2StockArea.gameObject.SetActive(true);
            }
            else
            {
                player1StockArea.gameObject.SetActive(false);
                player2StockArea.gameObject.SetActive(false);
            }
        }

        // プレイヤーの武器在庫が変化したときに呼ばれる
        private void UpdatePlayerStockArea(int playerNumber, Dictionary<string, int> weaponStock)
        {
            // PUN2 RPCを使って他のプレイヤーに同期
            if (PhotonNetwork.IsMasterClient)  // マスタークライアントが変更を通知
            {
                photonView.RPC("SyncWeaponStock", RpcTarget.All, playerNumber, weaponStock);
            }
        }

        // RPCで武器在庫を全プレイヤーに同期
        [PunRPC]
        private void SyncWeaponStock(int playerNumber, Dictionary<string, int> weaponStock)
        {
            if (playerNumber == 1)
            {
                player1StockArea.UpdatePlayerStockArea(weaponStock);
            }
            else if (playerNumber == 2)
            {
                player2StockArea.UpdatePlayerStockArea(weaponStock);
            }
        }
    }
}

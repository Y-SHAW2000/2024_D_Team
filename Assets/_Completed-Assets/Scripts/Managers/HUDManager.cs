using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace Complete
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private PlayerStockArea player1StockArea; // Player1 の HUD   PlayerStockArea
        [SerializeField] private PlayerStockArea player2StockArea; // Player2 の HUD
        [SerializeField] private Complete.GameManager gameManager; // GameManager への参照
        [SerializeField] private Complete.TankManager tankManager; //TankManagerへの参照

        private void Start()
        {
            if (gameManager != null)
            {
                gameManager.OnGameStateChanged += HandleGameStateChanged; //イベントの登録
                foreach (var tank in gameManager.m_Tanks)
                {
                    tank.OnWeaponStockChanged += UpdatePlayerStockArea;
                }//tankManager.OnWeaponStockChanged += UpdatePlayerStockArea;
            }
        }

        private void OnDestroy() //イベントの解除
        {
            if (gameManager != null)
            {
                gameManager.OnGameStateChanged -= HandleGameStateChanged;
            }
        }

        private void HandleGameStateChanged(Complete.GameManager.GameState gameState)    // ゲームの状態に応じて HUD の表示/非表示を切り替える
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
        private void UpdatePlayerStockArea(int playerNumber, int newStock)  // プレイヤー番号に応じて HUD の砲弾ストック数を更新
        {
            //UnityEngine.Debug.Log("player" + playerNumber + "Stock" + newStock);
            if (playerNumber == 1)
            {
                UnityEngine.Debug.Log("player" + playerNumber + "Stock" + newStock);
                player1StockArea.UpdatePlayerStockArea(newStock);
            }
            else if (playerNumber == 2)
            {
                player2StockArea.UpdatePlayerStockArea(newStock);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject player1StockArea; // Player1 の HUD
    [SerializeField] private GameObject player2StockArea; // Player2 の HUD
    [SerializeField] private Complete.GameManager gameManager; // GameManager への参照

    private void Start()
    {
        if (gameManager != null)
        {
            gameManager.OnGameStateChanged += HandleGameStateChanged; //イベントの登録
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
            player1StockArea.SetActive(true);
            player2StockArea.SetActive(true);
        }
        else
        {
            player1StockArea.SetActive(false);
            player2StockArea.SetActive(false);
        }
    }
}


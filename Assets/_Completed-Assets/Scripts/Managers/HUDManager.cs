using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject player1StockArea; // Player1 �� HUD
    [SerializeField] private GameObject player2StockArea; // Player2 �� HUD
    [SerializeField] private Complete.GameManager gameManager; // GameManager �ւ̎Q��

    private void Start()
    {
        if (gameManager != null)
        {
            gameManager.OnGameStateChanged += HandleGameStateChanged; //�C�x���g�̓o�^
        }
    }

    private void OnDestroy() //�C�x���g�̉���
    {
        if (gameManager != null)
        {
            gameManager.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    private void HandleGameStateChanged(Complete.GameManager.GameState gameState)    // �Q�[���̏�Ԃɉ����� HUD �̕\��/��\����؂�ւ���
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


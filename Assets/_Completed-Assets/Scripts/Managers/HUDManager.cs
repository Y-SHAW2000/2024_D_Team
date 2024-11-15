using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

namespace Complete
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private PlayerStockArea player1StockArea; // Player1 �� HUD   PlayerStockArea
        [SerializeField] private PlayerStockArea player2StockArea; // Player2 �� HUD
        [SerializeField] private Complete.GameManager gameManager; // GameManager �ւ̎Q��
        [SerializeField] private Complete.TankManager tankManager; //TankManager�ւ̎Q��

        private void Start()
        {
            if (gameManager != null)
            {
                gameManager.OnGameStateChanged += HandleGameStateChanged; //�C�x���g�̓o�^
                foreach (var tank in gameManager.m_Tanks)
                {
                    tank.OnWeaponStockChanged += UpdatePlayerStockArea;
                }//tankManager.OnWeaponStockChanged += UpdatePlayerStockArea;
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
                player1StockArea.gameObject.SetActive(true);
                player2StockArea.gameObject.SetActive(true);
            }
            else
            {
                player1StockArea.gameObject.SetActive(false);
                player2StockArea.gameObject.SetActive(false);
            }
        }
        private void UpdatePlayerStockArea(int playerNumber, Dictionary<string, int> weaponStock)  // �v���C���[�ԍ��ɉ����� HUD �̖C�e�X�g�b�N�����X�V
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
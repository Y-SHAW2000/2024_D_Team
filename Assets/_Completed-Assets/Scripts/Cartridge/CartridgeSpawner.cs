using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CartridgeSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnAreaMin; // スポーンエリアの最小値 (X, Z 座標)
    [SerializeField]
    private Vector2 spawnAreaMax; // スポーンエリアの最大値 (X, Z 座標)
    [SerializeField]
    private float spawnInterval = 5f; // カートリッジのスポーン間隔

    private Complete.GameManager gameManager; 

    void Start()
    {
        gameManager = FindObjectOfType<Complete.GameManager>();

        // ゲーム状態変更時のイベントハンドラーを登録
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        // イベントハンドラーを解除
        if (gameManager != null)
        {
            gameManager.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    private void HandleGameStateChanged(Complete.GameManager.GameState gameState)
    {
        if (PhotonNetwork.IsMasterClient) // マスタークライアントのみスポーン処理を行う
        {
            if (gameState == Complete.GameManager.GameState.RoundPlaying)
            {
                StartCoroutine(SpawnRoutine());
            }
            else
            {
                StopAllCoroutines(); // 他の状態ではスポーンを停止
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCartridge();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCartridge()
    {
        // ランダムなスポーン位置を計算
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ);

        // MineCartridge と ShellCartridge をスポーン
        PhotonNetwork.Instantiate("MineCartridge", spawnPosition, Quaternion.identity);
        PhotonNetwork.Instantiate("ShellCartridge", spawnPosition, Quaternion.identity);
    }
}

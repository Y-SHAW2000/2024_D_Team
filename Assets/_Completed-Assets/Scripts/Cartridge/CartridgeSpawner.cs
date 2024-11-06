using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class CartridgeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CartridgePrefab;
    [SerializeField]
    private float spawnInterval = 5f; // カートリッジの生成間隔
    [SerializeField]
    private Vector2 spawnAreaMin; // 生成範囲の最小値（X, Z座標）
    [SerializeField]
    private Vector2 spawnAreaMax; // 生成範囲の最大値（X, Z座標）

    private Complete.GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Complete.GameManager>(); //ゲームオブジェクトへの参照

        // OnGameStateChanged イベントに HandleGameStateChanged を登録
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(Complete.GameManager.GameState gameState) //子ルーチンの開始・停止
    {
        if (gameState == Complete.GameManager.GameState.RoundPlaying)
        {
            // ゲームプレイ中なら SpawnRoutine コルーチンを開始
            StartCoroutine(SpawnRoutine());
        }
        else
        {
            // ゲームがプレイ中でないならコルーチンを停止
            StopCoroutine(SpawnRoutine());
        }
    }


    // カートリッジをランダムな位置に生成するメソッド
    private void SpawnCartridge()
    {
        // ランダムな位置を計算
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);//x座標の置ける位置
        float randomZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);//z座標の置ける位置
        Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ); // 地面に配置するためY座標は0

        // カートリッジを生成
        Instantiate(CartridgePrefab, spawnPosition, Quaternion.identity);
    }

    // 一定間隔でカートリッジを生成するコルーチン
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCartridge(); // カートリッジを生成
            yield return new WaitForSeconds(spawnInterval); // spawnInterval秒待機
        }
    }
}

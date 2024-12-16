using System.Collections;
using UnityEngine;

public class GameManagerForRaid : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemy; 
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    private GameObject playerInstance;

    private bool gameOver = false; 


    void Awake()
    {
        StartGame();
    }

    void StartGame()
    {
        gameOver = false;

        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

    }

    void Update()
    {
        if (!gameOver)
        {
            CheckGameOver();
        }
    }

    void CheckGameOver()
    {
        if (playerInstance == null)
        {
            GameOver("Player has been defeated!");
        }
        else if (enemy == null)
        {
            GameOver("Enemy has been defeated!");
        }
    }

    void GameOver(string message)
    {
        gameOver = true;
        Debug.Log(message);
        Debug.Log("Game Over!");

    }

}

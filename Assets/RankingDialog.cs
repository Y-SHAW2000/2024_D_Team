using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RankingDialog : MonoBehaviour
{
    public GameObject[] rankingEntryPrefab;
    public GameObject playerEntryPrefab;
    public Transform rankingListParent;   
    public Button closeButton;

    public void ShowRanking(List<RankingManager.PlayerStats> rankings, RankingManager.PlayerStats currentPlayerStats)
    {
        // Add New Ranking
        for(int i = 0; i < 10; i++)
        {
            rankingEntryPrefab[i].GetComponent<RankingEntry>().SetData(rankings[i]);
            
        }

        // Add Player Data
        playerEntryPrefab.GetComponent<RankingEntry>().SetData(currentPlayerStats);



        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        });

        gameObject.SetActive(true);
    }
}

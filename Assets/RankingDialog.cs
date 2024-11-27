using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RankingDialog : MonoBehaviour
{
    public GameObject[] rankingEntryPrefab;
    public GameObject playerEntryPrefab;
    public Button closeButton;

    public void ShowRanking(List<RankingManager.PlayerStats> rankings, RankingManager.PlayerStats currentPlayerStats)
    {
        // Add New Ranking
        for(int i = 0; i < rankings.Count(); i++)
        {
            rankingEntryPrefab[i].GetComponent<RankingEntry>().SetData(rankings[i]);
        }

        // Add Player Data

        var playerData = rankings.FirstOrDefault(r => r.UserId == currentPlayerStats.UserId);

        if (playerData != null)
        {
            playerEntryPrefab.GetComponent<RankingEntry>().SetData(playerData);
            
        }
        else
        {
            playerEntryPrefab.GetComponent<RankingEntry>().SetData(currentPlayerStats, isOutOfTop10: true);
        }
        



        closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        });

        gameObject.SetActive(true);
    }
}

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using static RankingManager;

public class RankingManager : MonoBehaviour
{
    public class PlayerStats
    {
        public int Rank;         
        public string UserId;    
        public string UserName;  
        public float WinRate;    
        public int Wins;        
        public int Losses;      
    }

    public GameObject rankBoard;

    public void Rank(bool isWinner)
    {
        rankBoard.SetActive(true);

        transform.GetComponent<MatchResultManager>().UpdatePlayerStats(isWinner);

        transform.GetComponent<RankingDialog>().ShowRanking(GetTop10Ranking(), GetCurrentPlayerStats());

        //SaveRankings(RankingData);

    }

    [System.Serializable]
    public class RankingData
    {
        public PlayerStats[] playerStats;
    }

    private string saveFilePath;

    void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "rankings.json");

        LoadRankings();
    }

    public void SaveRankings(RankingData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Ranking data saved!");
    }

    public RankingData LoadRankings()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            RankingData data = JsonUtility.FromJson<RankingData>(json);
            Debug.Log("Ranking data loaded!");
            return data;
        }
        else
        {
            Debug.Log("No ranking data found. Starting fresh.");
            return new RankingData { playerStats = new PlayerStats[0] };
        }
    }

    public List<PlayerStats> GetTop10Ranking()
    {
        var players = PhotonNetwork.PlayerList;
        var statsList = new List<PlayerStats>();

        foreach (var player in players)
        {
            if (player.CustomProperties.TryGetValue(PlayerStatsKeys.Matches, out object matchCountObj) &&
                player.CustomProperties.TryGetValue(PlayerStatsKeys.Wins, out object winsObj) &&
                player.CustomProperties.TryGetValue(PlayerStatsKeys.Losses, out object lossesObj))
            {
                int matches = (int)matchCountObj;
                if (matches < 10) continue;

                int wins = (int)winsObj;
                int losses = (int)lossesObj;
                float winRate = wins / (float)matches;

                statsList.Add(new PlayerStats
                {
                    UserId = PhotonNetwork.AuthValues.UserId,
                    UserName = player.NickName,
                    Wins = wins,
                    Losses = losses,
                    WinRate = winRate
                });
            }
        }

        // sorting
        statsList = statsList.OrderByDescending(s => s.WinRate).ToList();
        for (int i = 0; i < statsList.Count; i++)
        {
            statsList[i].Rank = i + 1;
        }

        var rankedPlayers = statsList.Take(10).ToList();

        if (rankedPlayers.Count == 0)
        {
            Debug.LogWarning("No players meet the ranking criteria. Returning only the local player's data.");
            var currentPlayerStats = GetCurrentPlayerStats();

            if (currentPlayerStats != null)
            {
                return new List<PlayerStats> { currentPlayerStats };
            }

            return new List<PlayerStats>();
        }
        return rankedPlayers;
    }

    public PlayerStats GetCurrentPlayerStats()
    {
        var player = PhotonNetwork.LocalPlayer;

        int wins = player.CustomProperties.ContainsKey(PlayerStatsKeys.Wins) ? (int)player.CustomProperties[PlayerStatsKeys.Wins] : 0;
        int losses = player.CustomProperties.ContainsKey(PlayerStatsKeys.Losses) ? (int)player.CustomProperties[PlayerStatsKeys.Losses] : 0;
        int matches = player.CustomProperties.ContainsKey(PlayerStatsKeys.Matches) ? (int)player.CustomProperties[PlayerStatsKeys.Matches] : 0;

        float winRate = matches > 0 ? wins / (float)matches : 0;

        return new PlayerStats
        {
            Rank = 1,
            UserId = PhotonNetwork.AuthValues.UserId,
            UserName = player.NickName,
            WinRate = winRate,
            Wins = wins,
            Losses = losses
        };
    }
}

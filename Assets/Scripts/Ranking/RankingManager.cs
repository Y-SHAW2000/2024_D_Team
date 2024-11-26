using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
                    UserId = player.UserId,
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

        return statsList.Take(10).ToList();
    }
}

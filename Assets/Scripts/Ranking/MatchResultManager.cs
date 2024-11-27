using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MatchResultManager : MonoBehaviour
{
    public void UpdatePlayerStats(bool isWinner)
    {
        var player = PhotonNetwork.LocalPlayer;
        int wins = player.CustomProperties.ContainsKey(PlayerStatsKeys.Wins) ? (int)player.CustomProperties[PlayerStatsKeys.Wins] : 0;
        int losses = player.CustomProperties.ContainsKey(PlayerStatsKeys.Losses) ? (int)player.CustomProperties[PlayerStatsKeys.Losses] : 0;
        int matches = player.CustomProperties.ContainsKey(PlayerStatsKeys.Matches) ? (int)player.CustomProperties[PlayerStatsKeys.Matches] : 0;

        // データ更新
        if (isWinner) wins++;
        else losses++;
        matches++;

        // データ保存
        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { PlayerStatsKeys.Wins, wins },
            { PlayerStatsKeys.Losses, losses },
            { PlayerStatsKeys.Matches, matches }
        });
    }
}

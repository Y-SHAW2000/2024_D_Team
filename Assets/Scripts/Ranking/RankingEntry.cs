using UnityEngine;
using UnityEngine.UI;

public class RankingEntry : MonoBehaviour
{
    public Text rankText;
    public Text userIdText;
    public Text userNameText;
    public Text winRateText;
    public Text winsText;
    public Text lossesText;

    public void SetData(RankingManager.PlayerStats stats, bool isOutOfTop10 = false)
    {
        rankText.text = isOutOfTop10 ? "Œ—ŠO" : stats.Rank.ToString();
        userIdText.text = stats.UserId;
        userNameText.text = stats.UserName;
        winRateText.text = $"{stats.WinRate * 100:F2}%";
        winsText.text = stats.Wins.ToString();
        lossesText.text = stats.Losses.ToString();

        if (!isOutOfTop10 && stats.Rank == 1)
        {
            rankText.color = Color.yellow; 
        }
    }
}

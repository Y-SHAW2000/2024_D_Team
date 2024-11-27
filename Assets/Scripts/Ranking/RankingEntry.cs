using Unity;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingEntry : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text userIdText;
    public TMP_Text userNameText;
    public TMP_Text winRateText;
    public TMP_Text winsText;
    public TMP_Text lossesText;

    private void Start()
    {
        rankText = gameObject.transform.Find("RankText").GetComponent<TMP_Text>();
        userIdText = gameObject.transform.Find("UserIDText").GetComponent<TMP_Text>();
        userNameText = gameObject.transform.Find("UserNameText").GetComponent<TMP_Text>();
        winRateText = gameObject.transform.Find("WinRateText").GetComponent<TMP_Text>();
        winsText = gameObject.transform.Find("WinsText").GetComponent<TMP_Text>();
        lossesText = gameObject.transform.Find("LossesText").GetComponent<TMP_Text>();
    }

    public void SetData(RankingManager.PlayerStats stats, bool isOutOfTop10 = false)
    {
        Debug.Log(stats.Rank.ToString());
        Debug.Log(isOutOfTop10);
        rankText.text = isOutOfTop10 ? "OutOfTop10" : stats.Rank.ToString();
        if(stats.Rank == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(stats.Rank == 2)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if(stats.Rank == 3)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
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

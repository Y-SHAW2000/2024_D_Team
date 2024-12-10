using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // TextMeshPro用

public class AnnouncementManager : MonoBehaviour
{
    [System.Serializable]
    public class Announcement
    {
        public int id;
        public string title;
        public string content;
        public string timestamp;
    }

    [System.Serializable]
    public class AnnouncementList
    {
        public Announcement[] announcements;
    }

    public GameObject[] announcementUIPrefab; // UIプレハブ

    private string apiUrl = "http://127.0.0.1:5000/api/announcements"; // FlaskサーバーのURL

    void Start()
    {
        
        StartCoroutine(FetchAnnouncements());
    }

    IEnumerator FetchAnnouncements()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;

            // JSONをパースしてデータを取得
            Announcement[] announcements = JsonUtility.FromJson<AnnouncementList>(
                $"{{\"announcements\":{jsonResponse}}}"
            ).announcements;

            // UIに表示
            foreach (var announcement in announcements)
            {
                Debug.Log($"Title: {announcement.title}, Content: {announcement.content}");
            
                //GameObject newEntry = Instantiate(announcementUIPrefab, announcementListParent);
                //TMP_Text[] texts = newEntry.GetComponentsInChildren<TMP_Text>();
                //texts[0].text = announcement.title; // タイトル
                //[2].text = announcement.content; // 本文
                //
                //
                //
                //texts[1].text = announcement.timestamp; // 投稿日時
            }
            for(int i = 0; i < Mathf.Min(announcements.Length, announcementUIPrefab.Length); i++)
            {
                var titleText = announcementUIPrefab[i].transform.Find("TitileText").GetComponent<TMP_Text>();
                var dateText = announcementUIPrefab[i].transform.Find("DateText").GetComponent<TMP_Text>();
                var contentText = announcementUIPrefab[i].transform.Find("ContentText").GetComponent<TMP_Text>();

                titleText.text = announcements[i].title;
                dateText.text = announcements[i].timestamp;
                contentText.text = announcements[i].content;
            }
        }
        else
        {
            Debug.LogError($"Error fetching announcements: {request.error}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // TextMeshPro喘

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

    private string apiUrl = "http://127.0.0.1:5000/api/announcements"; // Flaskサ�`バ�`のURL

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

            // JSONをパ�`スしてデ�`タを函誼
            Announcement[] announcements = JsonUtility.FromJson<AnnouncementList>(
                $"{{\"announcements\":{jsonResponse}}}"
            ).announcements;

            // UIに燕幣
            foreach (var announcement in announcements)
            {
                Debug.Log($"Title: {announcement.title}, Content: {announcement.content}");
            
                //GameObject newEntry = Instantiate(announcementUIPrefab, announcementListParent);
                //TMP_Text[] texts = newEntry.GetComponentsInChildren<TMP_Text>();
                //texts[0].text = announcement.title; // タイトル
                //[2].text = announcement.content; // 云猟
                //
                //
                //
                //texts[1].text = announcement.timestamp; // 誘後晩�r
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

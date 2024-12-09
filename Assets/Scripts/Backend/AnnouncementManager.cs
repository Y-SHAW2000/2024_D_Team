using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // TextMeshPro��

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

    public GameObject[] announcementUIPrefab; // UI�ץ�ϥ�

    private string apiUrl = "http://127.0.0.1:5000/api/announcements"; // Flask���`�Щ`��URL

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

            // JSON��ѩ`�����ƥǩ`����ȡ��
            Announcement[] announcements = JsonUtility.FromJson<AnnouncementList>(
                $"{{\"announcements\":{jsonResponse}}}"
            ).announcements;

            // UI�˱�ʾ
            foreach (var announcement in announcements)
            {
                Debug.Log($"Title: {announcement.title}, Content: {announcement.content}");
            
                //GameObject newEntry = Instantiate(announcementUIPrefab, announcementListParent);
                //TMP_Text[] texts = newEntry.GetComponentsInChildren<TMP_Text>();
                //texts[0].text = announcement.title; // �����ȥ�
                //[2].text = announcement.content; // ����
                //
                //
                //
                //texts[1].text = announcement.timestamp; // Ͷ���Օr
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

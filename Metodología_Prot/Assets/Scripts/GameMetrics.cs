using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameMetrics : MonoBehaviour
{
    public static GameMetrics Instance;

    private float startTime;

    public float neutralTime;
    public int neutralErrors;

    public float spiderTime;
    public int spiderErrors;

    public float trypophobiaTime;
    public int trypophobiaErrors;

    [SerializeField]
    private string googleScriptURL;

  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void StartNeutralTimer()
    {
        startTime = Time.time;
    }

    public void FinishNeutral()
    {
        neutralTime = Time.time - startTime;

        SendMetrics(
            "Neutral",
            neutralTime,
            neutralErrors
        );
    }

    public void FinishSpider()
    {
        spiderTime = Time.time - startTime;

        SendMetrics(
            "Arachnophobia",
            spiderTime,
            spiderErrors
        );
    }
    public void FinishTrypophobia()
    {
        trypophobiaTime = Time.time - startTime;

        SendMetrics(
            "Trypophobia",
            trypophobiaTime,
            trypophobiaErrors
        );
    }

    public void AddTrypophobiaError()
    {
        trypophobiaErrors++;
    }

    public void AddNeutralError()
    {
        neutralErrors++;
    }

    public void AddSpiderError()
    {
        spiderErrors++;
    }

    private void SendMetrics(
        string room,
        float time,
        int errors)
    {
        StartCoroutine(SendToGoogleSheets(
            room,
            time,
            errors
        ));
    }

    private IEnumerator SendToGoogleSheets(
        string room,
        float time,
        int errors)
    {
        string json = JsonUtility.ToJson(
            new MetricData
            {
                id = "P001",
                room = room,
                time = time,
                errors = errors,
            }
        );

        using (UnityWebRequest request =

            new UnityWebRequest(
                 googleScriptURL,
                 "POST"))
            {
            byte[] body =
            System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler =
            new UploadHandlerRaw(body);

            request.downloadHandler =
            new DownloadHandlerBuffer();

            request.SetRequestHeader(
                 "Content-Type",
                 "application/json"
            );   

            yield return request.SendWebRequest();

            if (request.result ==
                UnityWebRequest.Result.Success)
            {
                Debug.Log("Métricas enviadas correctamente.");
            }
            else
            {
                Debug.LogError(
                    "Error enviando métricas: " +
                    request.error
                );
            }
        }
    }

    [System.Serializable]
    public class MetricData
    {
        public string id;
        public string room;
        public float time;
        public int errors;
    }

}
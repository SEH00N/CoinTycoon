using System.Collections;
using System.Text;
using Newtonsoft.Json;
using ProjectCoin.Networks.Payloads;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TServer : MonoBehaviour
{
    [SerializeField] TMP_InputField urlInputField = null;
    private const string BASE_URL = "https://localhost:7161/";

    public void SendRequest()
    {
        string url = $"{BASE_URL}{urlInputField.text}";
        StartCoroutine(WebRequestRoutine(url));
    }

    private IEnumerator WebRequestRoutine(string url)
    {
        RankingListRequest payload = new RankingListRequest();
        string payloadData = JsonConvert.SerializeObject(payload);

        using(UnityWebRequest request = UnityWebRequest.Post(url, payloadData, "application/json"))
        {
            Debug.Log(Encoding.UTF8.GetString(request.uploadHandler.data));
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Response: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"Error: {request.error}/{request.downloadHandler.text}");
            }
        }
    }
}

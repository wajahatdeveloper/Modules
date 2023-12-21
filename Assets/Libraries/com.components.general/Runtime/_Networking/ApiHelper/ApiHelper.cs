using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHelper : MonoBehaviour
{
    public static bool DisableDefaultErrorHandling = true;
    private static GameObject coroutineHelper;

    public static ApiHelper CoroutineHelper { get
        {
            if (coroutineHelper == null)
            {
                coroutineHelper = new GameObject("CoroutineHelper");
                coroutineHelper.AddComponent<ApiHelper>();
            }
            return coroutineHelper.GetComponent<ApiHelper>(); 
        }
        private set => coroutineHelper = value.gameObject; }

    public static void Get(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        CoroutineHelper.StartCoroutine(Get_CR(url, success, error, payload, headers));
    }

    public static void Post(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        CoroutineHelper.StartCoroutine(Post_CR(url, success, error, payload, headers));
	}

	public static void Patch(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        CoroutineHelper.StartCoroutine(Patch_CR(url, success, error, payload, headers));
	}

	public static void Delete(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        CoroutineHelper.StartCoroutine(Delete_CR(url, success, error, payload, headers));
	}

	public static void DownloadImage(string mediaUrl, Action<Texture2D> success, Action<string> error)
    {
        CoroutineHelper.StartCoroutine(DownloadImage_CR(mediaUrl, success, error));
	}

	public static IEnumerator Get_CR(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        if (headers != null)
        {
            foreach (KeyValuePair<string, string> pair in headers)
            {
                webRequest.SetRequestHeader(pair.Key, pair.Value);
            }
        }

        webRequest.SetRequestHeader("Content-Type", "application/json");

        if (payload != "")
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        Debug.Log($"Accessing Endpoint {url} with GET Request");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                if (!DisableDefaultErrorHandling)
                {
                    Debug.Log("Error Occured for Endpoint : " + url);
                    Debug.LogError("Error Body : " + webRequest.error);
                    error(webRequest.error);
                }
                else
                {
                    error(webRequest.downloadHandler.text);
                }
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Response Received for Endpoint : " + url);
                Debug.Log("Response Body : " + webRequest.downloadHandler.text);
                success(webRequest.downloadHandler.text);
                break;
        }
    }

    public static IEnumerator Post_CR(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        if (headers != null)
        {
            foreach (KeyValuePair<string, string> pair in headers)
            {
                webRequest.SetRequestHeader(pair.Key, pair.Value);
            }
        }

        webRequest.SetRequestHeader("Content-Type", "application/json");

        if (payload != "")
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        Debug.Log($"Accessing Endpoint {url} with POST Request");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                if (!DisableDefaultErrorHandling)
                {
                    Debug.Log("Error Occured for Endpoint : " + url);
                    Debug.LogError("Error Body : " + webRequest.error);
                    error(webRequest.error);
                }
                else
                {
                    error(webRequest.downloadHandler.text);
                }
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Response Received for Endpoint : " + url);
                Debug.Log("Response Body : " + webRequest.downloadHandler.text);
                success(webRequest.downloadHandler.text);
                break;
        }
    }

    public static IEnumerator Patch_CR(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(url, "PATCH");

        if (headers != null)
        {
            foreach (KeyValuePair<string, string> pair in headers)
            {
                webRequest.SetRequestHeader(pair.Key, pair.Value);
            }
        }

        webRequest.SetRequestHeader("Content-Type", "application/json");

        if (payload != "")
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        Debug.Log($"Accessing Endpoint {url} with PATCH Request");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                if (!DisableDefaultErrorHandling)
                {
                    Debug.Log("Error Occured for Endpoint : " + url);
                    Debug.LogError("Error Body : " + webRequest.error);
                    error(webRequest.error);
                }
                else
                {
                    error(webRequest.downloadHandler.text);
                }
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Response Received for Endpoint : " + url);
                Debug.Log("Response Body : " + webRequest.downloadHandler.text);
                success(webRequest.downloadHandler.text);
                break;
        }
    }

    public static IEnumerator Delete_CR(string url, Action<string> success, Action<string> error, string payload, Dictionary<string, string> headers = null)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(url, "DELETE");

        if (headers != null)
        {
            foreach (KeyValuePair<string, string> pair in headers)
            {
                webRequest.SetRequestHeader(pair.Key, pair.Value);
            }
        }

        webRequest.SetRequestHeader("Content-Type", "application/json");

        if (payload != "")
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        Debug.Log($"Accessing Endpoint {url} with DELETE Request");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                if (!DisableDefaultErrorHandling)
                {
                    Debug.Log("Error Occured for Endpoint : " + url);
                    Debug.LogError("Error Body : " + webRequest.error);
                    error(webRequest.error);
                }
                else
                {
                    error(webRequest.downloadHandler.text);
                }
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Response Received for Endpoint : " + url);
                Debug.Log("Response Body : " + webRequest.downloadHandler.text);
                success(webRequest.downloadHandler.text);
                break;
        }
    }

    public static IEnumerator DownloadImage_CR(string mediaUrl, Action<Texture2D> success, Action<string> error)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                if (!DisableDefaultErrorHandling)
                {
                    Debug.Log("Error Occured for Endpoint : " + mediaUrl);
                    Debug.LogError("Error Body : " + request.error);
                    error(request.error);
                }
                else
                {
                    error(request.downloadHandler.text);
                }
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Response Received for Endpoint : " + mediaUrl);
                Debug.Log("Response Body : " + request.downloadHandler.text);
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                success(texture);
                break;
        }
    }
}

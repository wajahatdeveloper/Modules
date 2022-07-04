using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Linq;

public class VideoLoad : MonoBehaviour
{
    public VideoPlayer bgVideo;
    public GameObject[] objectsToEnable;
    
    private IEnumerator Start()
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, "background.mp4");
        bgVideo.url = url;

        bgVideo.targetCamera = Camera.main;
        ///LoadingPanel.Instance.Show("Loading");
        yield return new WaitUntil(() => bgVideo.isPrepared);
        yield return new WaitForSecondsRealtime(1.0f);
        ///LoadingPanel.Instance.Hide();
        objectsToEnable.ToList().ForEach(x => x.SetActive(true));
    }
}

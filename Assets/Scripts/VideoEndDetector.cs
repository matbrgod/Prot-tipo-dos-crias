using UnityEngine;
using UnityEngine.Video;

public class VideoEndDetector : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void OnEnable()
    {
        // Subscribe to the loopPointReached event
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnDisable()
    {
        // Unsubscribe from the loopPointReached event
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LevelManager.Instance.LoadScene("Menu", "CrossFade");
    }
}
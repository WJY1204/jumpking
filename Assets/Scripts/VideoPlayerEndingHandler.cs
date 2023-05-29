using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerEndingHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public string animationName;
    public string nextLevel;

    private void Start()
    {
        // Subscribe to the video player's loop point reached event
        videoPlayer.loopPointReached += OnVideoPlayerEnd;
    }

    private void OnVideoPlayerEnd(VideoPlayer vp)
    {
        if (vp == videoPlayer)
        {
            // The video has ended, implement your desired logic here
            Debug.Log("Video ended!");

            // Example: Load a new scene or display a UI element
            // SceneManager.LoadScene("NextScene");
            // endScreenUI.SetActive(true);
            
                

            PlayerPrefs.SetInt(animationName, 1);
            SceneTransitionManager.TriggerSceneTransition(nextLevel, 1f);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the video player's loop point reached event
        videoPlayer.loopPointReached -= OnVideoPlayerEnd;
    }
}

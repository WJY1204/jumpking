using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;




public class Video : MonoBehaviour {

    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += EndReached;
        
    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
          SceneManager.LoadScene(1);
    }
}
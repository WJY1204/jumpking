using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayClip(AudioClip clip, bool loop = false)
    {
        GameObject audioObject = new GameObject("AudioSource" + clip.name);
        audioObject.transform.parent = this.transform;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();

        if(!loop)
        {
            StartCoroutine(DestroyAfterClip(audioObject, clip.length));
        }
    }

    private IEnumerator DestroyAfterClip(GameObject audioObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(audioObject);
    }
}
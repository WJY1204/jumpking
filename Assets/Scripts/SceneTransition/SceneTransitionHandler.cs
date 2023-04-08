using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionHandler : MonoBehaviour
{
    public string nextSceneName; // The name of the scene you want to load
    public float transitionDelay = 0f; // Time to wait before transitioning, in seconds
    public float fadeDuration = 1f; // The duration of the fade effect, in seconds
    public Image maskImage; // The Image component to be used as a mask

    private void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        // Fade in the mask image
        yield return StartCoroutine(FadeMask(0f, 1f, fadeDuration));

        // Load the next scene after the delay
        yield return StartCoroutine(LoadSceneAfterDelay(nextSceneName, transitionDelay));

        // Fade out the mask image
        yield return StartCoroutine(FadeMask(1f, 0f, fadeDuration));

        // Destroy the transition handler object
        Destroy(gameObject);
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator FadeMask(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            maskImage.color = new Color(maskImage.color.r, maskImage.color.g, maskImage.color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        maskImage.color = new Color(maskImage.color.r, maskImage.color.g, maskImage.color.b, endAlpha);
    }
}

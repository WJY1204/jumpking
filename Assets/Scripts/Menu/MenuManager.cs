using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Vector2[] levelPositions;
    [SerializeField] private RectTransform content;
    [SerializeField] private float lerpDuration = 1f;

    public ScrollRect scrollRect; // The ScrollRect component to be modified
    
    private int currentPos, lastUnlockPos;

    private void Awake() 
    {
        currentPos = PlayerPrefs.GetInt("CurrentUnlockPos", 0);    
        lastUnlockPos = PlayerPrefs.GetInt("lastUnlockPos", 0);   

        content.anchoredPosition = levelPositions[currentPos];
        ScrollToPoint(currentPos, lastUnlockPos);

        PlayerPrefs.SetInt("CurrentUnlockPos", lastUnlockPos);
    }

    public void ScrollToPoint(int oldVal, int newVal)
    {
        StartCoroutine(ScrollCoroutine(oldVal, newVal));
    }

    IEnumerator ScrollCoroutine(int oldVal,int newVal)
    {
        scrollRect.vertical = false;
        yield return new WaitForSeconds(1f);

        float elapsedTime = 0;
        Vector2 valueToLerp = Vector2.zero;
        Vector2 startPosition = levelPositions[oldVal];
        Vector2 targetPos =  levelPositions[newVal];
       
        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / lerpDuration;

            content.anchoredPosition = Vector2.Lerp(startPosition, targetPos, t);

            yield return null;
        }

        scrollRect.vertical = true;
    }
}

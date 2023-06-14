using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private Button[] levelBtns;
    [SerializeField] private Vector2[] levelPositions;
    [SerializeField] private RectTransform content;
    [SerializeField] private float lerpDuration = 1f;
    [SerializeField] private GameObject maskPanel;

    public ScrollRect scrollRect; // The ScrollRect component to be modified
    
    private int currentPos, lastUnlockPos;


private void OnEnable() {
        Time.timeScale = 1;
    }
    private void Awake() 
    {
        currentPos = PlayerPrefs.GetInt("CurrentUnlockPos", 0);    
        lastUnlockPos = PlayerPrefs.GetInt("lastUnlockPos", 0);   

        content.anchoredPosition = levelPositions[currentPos];

        if(currentPos != lastUnlockPos)
        {
            ScrollToPoint(currentPos, lastUnlockPos);
            PlayerPrefs.SetInt("CurrentUnlockPos", lastUnlockPos);
        }

        ActiveButtons();
    }
void Update()
{
    if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
    {
         ResetGameRecord();
         LoadFirstScene();
    }
}
void ResetGameRecord()
{
    // 清除PlayerPrefs中的所有数据
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();

    // 执行其他重置游戏记录的操作
    // 这里可以是重置得分、重置关卡等等

    Debug.Log("重置游戏记录！");
}
void LoadFirstScene()
{
    // 加载第一个场景
    SceneManager.LoadScene(0);
}
    public void LoadScene(string name)
    {
        if(name == "Level1")
            SceneTransitionManager.TriggerSceneTransition("Level1", 1f);
        else if(PlayerPrefs.GetInt(name+" Video", 0) == 0)
            SceneTransitionManager.TriggerSceneTransition(name+" Video", 1f);
        else
            SceneTransitionManager.TriggerSceneTransition(name, 1f);
    }

    public void ActiveButtons()
    {
        int i = 0;
        for(i=0;i<=lastUnlockPos;i++)
        {
            levelBtns[i].interactable = true;
        }
        for(;i<levelBtns.Length;i++)
        {
            levelBtns[i].interactable = false;
        }
    }

    public void ScrollToPoint(int oldVal, int newVal)
    {
        StartCoroutine(ScrollCoroutine(oldVal, newVal));
    }

    IEnumerator ScrollCoroutine(int oldVal,int newVal)
    {
        maskPanel.SetActive(true);
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
        maskPanel.SetActive(false);
    }
}

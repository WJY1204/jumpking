using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Goal : MonoBehaviour
{
    public int unlockNextLevel = 0;
    
    public TMP_Text time;
    public GameObject goalCanvas;

    public TimerMachine timerMachine;

    void Update()
{
    if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
    {
        timerMachine.Pause();
            time.text = (Mathf.Round(timerMachine.timer * 100f) / 100f).ToString();
            PlayerPrefs.SetInt("lastUnlockPos", unlockNextLevel);
            goalCanvas.SetActive(true); //開啟介面
        Debug.Log("O和P同时按下！");
    }
}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timerMachine.Pause();
            time.text = (Mathf.Round(timerMachine.timer * 100f) / 100f).ToString();
            PlayerPrefs.SetInt("lastUnlockPos", unlockNextLevel);
            goalCanvas.SetActive(true); //開啟介面
        }
    }

    public void SceneTransit()
    {
        SceneTransitionManager.TriggerSceneTransition("Menu", 1f);
    }

}

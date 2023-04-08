using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int unlockNextLevel = 0;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("lastUnlockPos", unlockNextLevel);
            SceneTransitionManager.TriggerSceneTransition("Menu", 1f);
        }
    }
}

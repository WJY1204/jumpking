using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isOpen = false;
    public void OpenExit(GameObject exit)
    {

        if (isOpen == false)
        {
            exit.SetActive(true);
        }
        else
        {
            exit.SetActive(false);
        }

        isOpen =!isOpen;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //暫停
    public void Pause()
    {
     //暫停時間
     Time.timeScale = 0;
    }

    //回到遊戲
    public void BackToGame()
    {
        Time.timeScale = 1;
    }

    //離開遊戲
    public void ExitGame()
    {
        Application.Quit();
    }

    //重新開始遊戲
    public void InitGame()
    {
       
    }
}


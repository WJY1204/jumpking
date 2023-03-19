using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TimerMachine : MonoBehaviour
{
    public Text NewTime;
    

    //當前秒數
    public float Timer;
    //允許碼表計時
    public bool AllowRun;
    
    
    

    void Start()
    {
      Timer = 0;  
    }

    
    void Update()
    {

    if(AllowRun)
    {
       Timer += Time.deltaTime;
       Debug.Log(Timer);
       NewTime.text = Timer.ToString();

       
    }
    
    }


    //初始化(歸零)
    public void Init()
    {
    Timer = 0;
    AllowRun = false;
    }

    //讓碼表開始計時
    public void Run()
    {
        AllowRun = true;
    }

    //暫停
    public void Pause()
    {
        AllowRun = false;
    }
    
}

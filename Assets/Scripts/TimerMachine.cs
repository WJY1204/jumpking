
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TimerMachine : MonoBehaviour
{
    public string newTime;
     public TMP_Text timeOnGame;
    

    //當前秒數
    public float timer;
    //允許碼表計時
    public bool allowRun;
    
    private void Awake() 
    {
        timeOnGame = GetComponent<TMP_Text>();
    
    }
    

    void Start()
    {
      timer = 0;  
    }


    
    void Update()
    {

    if(allowRun)
    {
       timer += Time.deltaTime;
       Debug.Log(timer);
       newTime = timer.ToString("0.00");
       timeOnGame.text = newTime;
       
    }
    
    }


    //初始化(歸零)
    public void Init()
    {
    timer = 0;
    allowRun = false;
    }

    //讓碼表開始計時
    public void Run()
    {
        allowRun = true;
    }

    //暫停
    public void Pause()
    {
        allowRun = false;
    }
    
}

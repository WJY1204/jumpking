using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSkip : MonoBehaviour
{
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        print("GameStart");
        Application.LoadLevel("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMap : MonoBehaviour
{
   
 // Start is called before the first frame update
    void Start()
    {
        
    }

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
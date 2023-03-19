using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
   

  
    public void GameStart()
    {
        print("GameStart");
        Application.LoadLevel("FirstVideo");
    }
}

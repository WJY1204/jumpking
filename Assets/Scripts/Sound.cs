using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sound : MonoBehaviour
{
   
   [SerializeField] AudioSource Music;
    [SerializeField] AudioSource Audio_Touch;
   public void Onmusic()
   {
      Music.Play();
   }

   public void Offmusic()
   {
      Music.Stop();
   }
}

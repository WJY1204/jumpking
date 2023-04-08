using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    public Vector2 GetSpawnPoint 
    { 
        get 
        {
            return spawnPoint.position;
        } 
    }
}

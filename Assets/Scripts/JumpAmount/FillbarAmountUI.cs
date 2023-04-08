using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillbarAmountUI : MonoBehaviour
{
    public Transform Follow;
    public Vector2 offset;

    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Follow.position;//MainCamera.WorldToScreenPoint(Follow.position);

        transform.position = screenPos + (Vector3)offset;
    }
}

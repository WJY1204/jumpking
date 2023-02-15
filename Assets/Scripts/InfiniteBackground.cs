using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    GameObject camera;
    float imageHeight = 20f;
    float startPosition;
    [SerializeField]
    float parallaxEffect = 0.3f;

    void Start()
    {
        startPosition = transform.position.y;
    }

    void FixedUpdate (){
        float temp  = (camera.transform.position.y * (1 - parallaxEffect));

        float distance = (camera.transform.position.y * parallaxEffect);

        transform.position = new Vector3 (transform.position.x, startPosition + distance,transform.position.z);

        if (temp > startPosition + imageHeight){
            startPosition += imageHeight;
        }
        else if (temp < startPosition - imageHeight){
            startPosition -= imageHeight;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

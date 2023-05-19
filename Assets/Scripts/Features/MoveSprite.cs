using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public enum MovementMode { OneWay, PingPong }
    public MovementMode movementMode;
    
    private Vector2 startPosition;
    public Vector2 endPosition;
    public float speed = 1.0f;

    private Vector2 targetPosition;
    private bool movingToStart;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = endPosition;
        movingToStart = false;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.localScale = new Vector3((targetPosition.x - transform.position.x < 0 ? 1 : -1),1,1);

        if ((Vector2)transform.position == targetPosition)
        {
            if (movementMode == MovementMode.PingPong)
            {
                if (movingToStart)
                {
                    targetPosition = endPosition;
                    movingToStart = false;
                }
                else
                {
                    targetPosition = startPosition;
                    movingToStart = true;
                }
            }
        }
    }
}

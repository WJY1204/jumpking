using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public enum ScriptFeature
    {
        Rotation,
        PingPongRotation,
        Scaling,
        Both
    }

    public ScriptFeature selectedFeature = ScriptFeature.Rotation;
    public float rotationSpeed = 360f;
    public float pingPongRotationSpeed = 45f;
    public float pingPongRotationMin = -45f;
    public float pingPongRotationMax = 45f;
    public Vector2 scaleRange = new Vector2(0.5f, 2f);

    [Range(0f, 1f)]
    public float scaleSpeed = 0.5f;

    private float scaleTime;
    private float rotationTime;

    void Update()
    {
        switch (selectedFeature)
        {
            case ScriptFeature.Rotation:
                Rotate();
                break;
            case ScriptFeature.PingPongRotation:
                PingPongRotate();
                break;
            case ScriptFeature.Scaling:
                PingPongScale();
                break;
            case ScriptFeature.Both:
                Rotate();
                PingPongScale();
                break;
        }
    }

    void Rotate()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationThisFrame);
    }

    void PingPongRotate()
    {
        rotationTime += Time.deltaTime * pingPongRotationSpeed;
        float rotationValue =
            Mathf.PingPong(rotationTime, pingPongRotationMax - pingPongRotationMin)
            + pingPongRotationMin;
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
    }

    void PingPongScale()
    {
        scaleTime += Time.deltaTime * scaleSpeed;
        float scaleValue = Mathf.PingPong(scaleTime, scaleRange.y - scaleRange.x) + scaleRange.x;
        transform.localScale = new Vector3(scaleValue, scaleValue, 1);
    }

    public void UpdateRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }

    public void UpdateSelectedFeature(int featureIndex)
    {
        selectedFeature = (ScriptFeature)featureIndex;
    }
}
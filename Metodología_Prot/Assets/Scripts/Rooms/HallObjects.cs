using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HallObjects : MonoBehaviour
{
    public float riseSpeed = 0.5f;
    public float lowerSpeed = 0.3f; 
    public bool isRising = false;
    public bool isLowering = false;
    public float minY = -0.9f;

    void Update()
    {
        if (isRising)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
        else if (isLowering)
        {
            if (transform.position.y > minY)
            {
                transform.position += Vector3.down * lowerSpeed * Time.deltaTime;
            }
            else
            {
                isLowering = false;
            }
        }
    }

    public void StartRising()
    {
        isRising = true;
        isLowering = false;
    }

    public void StopRising()
    {
        isRising = false;
        isLowering = true;
    }
}
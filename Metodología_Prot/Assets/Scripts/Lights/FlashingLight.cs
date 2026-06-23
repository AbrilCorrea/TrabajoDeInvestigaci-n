using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Light spotlight;               
    public float minOffTime = 0.1f;      
    public float maxOffTime = 0.5f;         
    public float minOnTime = 0.2f;        
    public float maxOnTime = 1f;         

    private void Start()
    {
        if (spotlight == null)
            spotlight = GetComponent<Light>();

        StartCoroutine(Flicker());
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            spotlight.enabled = false;
            float offTime = Random.Range(minOffTime, maxOffTime);
            yield return new WaitForSeconds(offTime);

            spotlight.enabled = true;
            float onTime = Random.Range(minOnTime, maxOnTime);
            yield return new WaitForSeconds(onTime);
        }
    }
}

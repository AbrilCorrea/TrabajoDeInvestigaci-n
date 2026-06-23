using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashlight; 
    private bool isOn = true; 

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>(); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}

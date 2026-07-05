using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerNotes : MonoBehaviour
{
    [TextArea(3, 10)]
    public string noteText;

    private bool playerInRange = false;
    private bool hasBeenNoted = false;

    private Transform playerCamera;

    void Update()
    {
        if (!playerInRange || hasBeenNoted) return;

        Vector3 toNote = (transform.position - playerCamera.position).normalized;
        Vector3 cameraForward = playerCamera.forward;

        float dot = Vector3.Dot(cameraForward, toNote);

        if (dot > 0.7f) 
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                hasBeenNoted = true;

            }
        }
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenNoted)
        {
            playerInRange = true;

            playerCamera = Camera.main.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
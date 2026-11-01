using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNotes : MonoBehaviour
{
    public float interactDistance = 3f;
    public Transform player;
    public CanvasGroup interactUI;
    public GameObject ImageCanvas;

    private bool isReading = false;
    private bool wasInRange = false;

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.alpha = 0f;
            interactUI.interactable = false;
            interactUI.blocksRaycasts = false;
        }

        if (ImageCanvas != null)
            ImageCanvas.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (!isReading)
        {
            if (distance <= interactDistance)
            {
                if (!wasInRange)
                {
                    wasInRange = true;
                    FadeUI(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    FadeUI(false);
                    wasInRange = false;
                    isReading = true;
                    ImageCanvas.SetActive(true);
                }
            }
            else if (wasInRange)
            {
                wasInRange = false;
                FadeUI(false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isReading = false;
                ImageCanvas.SetActive(false);

                if (distance <= interactDistance)
                {
                    wasInRange = true;
                    FadeUI(true);
                }
            }
        }
    }

    void FadeUI(bool fadeIn)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(fadeIn));
    }

    System.Collections.IEnumerator FadeCoroutine(bool fadeIn)
    {
        float duration = 0.3f;
        float startAlpha = interactUI.alpha;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            interactUI.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        if (!fadeIn)
        {
            interactUI.interactable = false;
            interactUI.blocksRaycasts = false;
        }
        else
        {
            interactUI.interactable = true;
            interactUI.blocksRaycasts = true;
        }

        interactUI.alpha = endAlpha;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2f);
    }
}
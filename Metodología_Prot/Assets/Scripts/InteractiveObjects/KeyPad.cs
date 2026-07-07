using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string answer;
    [SerializeField] private InteractiveDoor doorInteraction;

    public FirstPersonController playerController;

    private bool isNear = false;
    public float interactDistance = 3f;
    public Transform player;
    public CanvasGroup interactUI;

    public GameObject canvas;
    private bool isCanvasOpen = false;
    private bool hasUnlocked = false;

    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.alpha = 0f;
            interactUI.interactable = false;
            interactUI.blocksRaycasts = false;
        }

        if (doorInteraction != null)
            doorInteraction.enabled = false;

        GameMetrics.Instance.StartNeutralTimer();
    
    }
    private void Update()
    {
        if (hasUnlocked) return; 

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactDistance)
        {
            if (!isNear)
            {
                isNear = true;
                FadeUI(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                isCanvasOpen = !isCanvasOpen;

                if (isCanvasOpen)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvas.gameObject.SetActive(true);

                }
                else
                {
                    CloseCanvas();
                }
            }
        }
        else
        {
            if (isNear)
            {
                isNear = false;
                FadeUI(false);
                CloseCanvas();
            }
        }
    }
    public void Number(int number)
    {
        text.text += number.ToString();
    }

    public void Enter()
    {
        if (text.text == answer)
        {
            text.text = "Correct";

            if (GameMetrics.Instance != null)
            {
                GameMetrics.Instance.FinishNeutral();

                Debug.Log("Sala Neutral");
                Debug.Log("Tiempo: " + GameMetrics.Instance.neutralTime);
                Debug.Log("Errores: " + GameMetrics.Instance.neutralErrors);
            }

            if (doorInteraction != null)
            {
                doorInteraction.enabled = true;
            }
            else
            {
                Debug.LogWarning("No hay puerta asignada en KeyPad");
            }

            hasUnlocked = true;
            CloseCanvas();
        }
        else
        {
            text.text = "Incorrect";

            if (GameMetrics.Instance != null)
                GameMetrics.Instance.AddNeutralError();
        }
    }
    public void Delete()
    {
        text.text = "";
    }
    private void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isCanvasOpen = false;

    }

    void FadeUI(bool fadeIn)
    {
        StopAllCoroutines();
        //StartCoroutine(FadeCoroutine(fadeIn));
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

        interactUI.alpha = endAlpha;
        interactUI.interactable = fadeIn;
        interactUI.blocksRaycasts = fadeIn;
        if (!fadeIn)
        {
            text.text = ""; 
            interactUI.interactable = false;
            interactUI.blocksRaycasts = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2f);
    }
}

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

    private bool blockPlayer = false;

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
    private void LateUpdate()
    {
        if (blockPlayer && playerController != null)
        {
            playerController.canMove = false;
        }
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
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                isCanvasOpen = !isCanvasOpen;

                if (isCanvasOpen)
                {
                    blockPlayer = true;

                    if (playerController != null)
                        playerController.canMove = false;

                    canvas.gameObject.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
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

        blockPlayer = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null)
            playerController.canMove = true;

        isCanvasOpen = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2f);
    }
}

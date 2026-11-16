using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPad2 : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string answer;
    [SerializeField] private InteractiveDoor doorInteraction;
    [SerializeField] private TMP_Text text2;

    public FirstPersonController playerController;

    private bool isNear = false;
    public float interactDistance = 3f;
    public Transform player;
    public CanvasGroup interactUI;

    public GameObject canvas;
    private bool isCanvasOpen = false;
    private bool hasUnlocked = false;

    internal bool canUseKeypad = false;
    private bool blockPlayer = false;

    private void Start()
    {
        GameMetrics.Instance.StartTimer();
    }
    private void LateUpdate()
    {
        if (blockPlayer && playerController != null)
        {
            playerController.canMove = false;
        }
    }
    void Update()
    {
        if (!canUseKeypad)
            return;

        if (hasUnlocked)
        {
            ShowInteractUI(false);
            return;
        }
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactDistance)
        {
            if (!isNear)
            {
                isNear = true;
                ShowInteractUI(true);
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
                ShowInteractUI(false);
                CloseCanvas();
            }
        }
    }
    public void Letter(string letter)
    {
        text2.text = "";
        text.text += letter;
    }
    public void Enter()
    {
        if (text.text == answer)
        {
            text2.text = "Correcto";
            text.text = "";

            if (GameMetrics.Instance != null)
            {
                GameMetrics.Instance.FinishSpider();

                Debug.Log("Sala Arachnophobia");
                Debug.Log("Tiempo: " + GameMetrics.Instance.spiderTime);
                Debug.Log("Errores: " + GameMetrics.Instance.spiderErrors);
            }

            doorInteraction.canInteract = true;
            hasUnlocked = true;
            CloseCanvas();
        }
        else
        {
            text2.text = "Incorrecto";
            text.text = "";

            if (GameMetrics.Instance != null)
            {
                GameMetrics.Instance.AddSpiderError();
            }
        }
    }
    public void Delete()
    {
        text.text = "";
        text2.text = "";
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
        ShowInteractUI(isNear && !hasUnlocked);
    }
    private void ShowInteractUI(bool show)
    {
        if (interactUI != null)
        {
            interactUI.alpha = show ? 1f : 0f;
            interactUI.interactable = show;
            interactUI.blocksRaycasts = show;
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

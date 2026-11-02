using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPad3 : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string answer;
    [SerializeField] private InteractiveDoor doorInteraction;
    [SerializeField] private TMP_Text text2;

    public FirstPersonController playerController;

    private bool isNear = false;
    public float interactDistance = 3f;
    public Transform player;

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
    public void Number(string number)
    {
        text.text += number.ToString();
    }

    public void Enter()
    {
        if (text.text == answer)
        {
            text2.text = "Correct";
            text.text = "";

            if (GameMetrics.Instance != null)
            {
                GameMetrics.Instance.FinishTrypophobia();

                Debug.Log("Sala Tripofobia");
                Debug.Log("Tiempo: " + GameMetrics.Instance.trypophobiaTime);
                Debug.Log("Errores: " + GameMetrics.Instance.trypophobiaErrors);
            }

            doorInteraction.canInteract = true;
            hasUnlocked = true;
            CloseCanvas();
        }
        else
        {
            text2.text = "Incorrect";
            text.text = "";

            if (GameMetrics.Instance != null)
            {
                GameMetrics.Instance.AddTrypophobiaError();
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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2f);
    }

}

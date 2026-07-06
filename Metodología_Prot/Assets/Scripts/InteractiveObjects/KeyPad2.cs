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


    private void Start()
    {
        if (interactUI != null)
        {
            interactUI.alpha = 0f;
            interactUI.interactable = false;
            interactUI.blocksRaycasts = false;
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
            doorInteraction.canInteract = true;
            hasUnlocked = true;
            CloseCanvas();
        }
        else
        {
            text2.text = "Incorrect";
            text.text = "";
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

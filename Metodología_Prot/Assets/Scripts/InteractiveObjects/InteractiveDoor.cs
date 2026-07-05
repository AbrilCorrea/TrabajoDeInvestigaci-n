using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    public float openAngle = -90f;
    public float openSpeed = 2f;
    public float interactDistance = 3f;
    public Transform player;
    //public CanvasGroup interactUI;

    private bool isOpen = false;
    private bool isMoving = false;
    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private bool isNear = false;
    public AudioSource Sound;

    public Transform interactionOrigin;

    void Start()
    {
        closedRotation = transform.rotation;
        targetRotation = closedRotation;

        //if (interactUI != null)
        //{
        //    interactUI.alpha = 0f;
        //    interactUI.interactable = false;
        //    interactUI.blocksRaycasts = false;
        //}
    }

    void Update()
    {
        Vector3 origin = interactionOrigin != null ? interactionOrigin.position : transform.position;
        float distance = Vector3.Distance(player.position, origin);

        if (distance <= interactDistance)
        {
            if (!isNear)
            {
                isNear = true;
                //FadeUI(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isMoving)
                {
                    if (!isOpen)
                        OpenDoor();
                    else
                        CloseDoor();
                }
            }
        }
        else
        {
            if (isNear)
            {
                isNear = false;
                //FadeUI(false);
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        targetRotation = Quaternion.Euler(closedRotation.eulerAngles + new Vector3(0, openAngle, 0));

        Sound.Play();
        StartCoroutine(MoveDoor());
    }

    void CloseDoor()
    {
        isOpen = false;
        targetRotation = closedRotation;
        Sound.Play();
        StartCoroutine(MoveDoor());
    }

    public void CloseDoorExternally()
    {
        CloseDoor();
        if (!isMoving)
        {
           
        }
    }

    System.Collections.IEnumerator MoveDoor()
    {
        isMoving = true;
        gameObject.layer = LayerMask.NameToLayer("DoorMoving");

        Quaternion startRotation = transform.rotation;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }
        gameObject.layer = LayerMask.NameToLayer("Default");

        isMoving = false;
    }

    Coroutine fadeRoutine;

    //void FadeUI(bool fadeIn)
    //{
    //    if (fadeRoutine != null)
    //        StopCoroutine(fadeRoutine);

    //    fadeRoutine = StartCoroutine(FadeCoroutine(fadeIn));
    //}
    //System.Collections.IEnumerator FadeCoroutine(bool fadeIn)
    //{
    //    float duration = 0.3f;
    //    float startAlpha = interactUI.alpha;
    //    float endAlpha = fadeIn ? 1f : 0f;
    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        interactUI.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
    //        yield return null;
    //    }

    //    interactUI.alpha = endAlpha;
    //}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 origin = interactionOrigin != null ? interactionOrigin.position : transform.position;

        Gizmos.DrawWireSphere(origin, interactDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + transform.forward * 2f);
    }
}
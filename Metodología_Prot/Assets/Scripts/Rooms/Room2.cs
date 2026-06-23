using System.Collections;
using UnityEngine;
public class Room2 : MonoBehaviour
{
    public Animator animator;

    public Animator animatorDoors;

    public Collider triggerZone;
    public InteractiveDoor doorScript;
    public GameObject Text;

    public float wallsAnimationDuration = 3f; 

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (triggerZone != null)
                triggerZone.enabled = false;

            doorScript.CloseDoorExternally();
            animatorDoors.SetTrigger("DoorsClosed");

            if (doorScript != null)
            {
                doorScript.enabled = false;
                Text.SetActive(false);
            }

            animator.SetTrigger("CloseWalls");

            StartCoroutine(ReenableAfterAnimation());
        }
    }

    private IEnumerator ReenableAfterAnimation()
    {
        yield return new WaitForSeconds(wallsAnimationDuration);

        if (doorScript != null)
            doorScript.enabled = true;

        if (Text != null)
            Text.SetActive(true);

        animatorDoors.SetTrigger("DoorOpen");
    }
}
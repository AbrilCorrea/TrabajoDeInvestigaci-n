using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsElevator : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform centerPoint;

    public float speed = 2f;
    public float centerOffset = 0.5f;

    private Vector3 leftOriginalPosition;
    private Vector3 rightOriginalPosition;

    private Coroutine movementCoroutine;

    private void Start()
    {
        leftOriginalPosition = leftDoor.position;
        rightOriginalPosition = rightDoor.position;
    }

    public void CloseDoors()
    {

        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveDoors(true));
    }

    public void OpenDoors()
    {
        if (movementCoroutine != null)
            StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveDoors(false));
    }

    private IEnumerator MoveDoors(bool close)
    {
        float leftTargetX;
        float rightTargetX;

        if (close)
        {
            leftTargetX = centerPoint.position.x - centerOffset;
            rightTargetX = centerPoint.position.x + centerOffset;
        }
        else
        {
            leftTargetX = leftOriginalPosition.x;
            rightTargetX = rightOriginalPosition.x;
        }

        while (Mathf.Abs(leftDoor.position.x - leftTargetX) > 0.01f ||
               Mathf.Abs(rightDoor.position.x - rightTargetX) > 0.01f)
        {
            leftDoor.position = new Vector3(
                Mathf.MoveTowards(leftDoor.position.x, leftTargetX, speed * Time.deltaTime),
                leftDoor.position.y,
                leftDoor.position.z
            );

            rightDoor.position = new Vector3(
                Mathf.MoveTowards(rightDoor.position.x, rightTargetX, speed * Time.deltaTime),
                rightDoor.position.y,
                rightDoor.position.z
            );

            yield return null;
        }

        leftDoor.position = new Vector3(leftTargetX, leftDoor.position.y, leftDoor.position.z);
        rightDoor.position = new Vector3(rightTargetX, rightDoor.position.y, rightDoor.position.z);

        movementCoroutine = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClaustro : MonoBehaviour
{
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform backWall;

    [SerializeField] private Transform centerPoint;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float moveDistance = 3f;

    private Vector3 leftStart;
    private Vector3 rightStart;
    private Vector3 backStart;

    private bool closing = false;

    void Start()
    {
        leftStart = leftWall.position;
        rightStart = rightWall.position;
        backStart = backWall.position;

    }

    void Update()
    {
        if (!closing) return;

        MoveWall(leftWall, leftStart);
        MoveWall(rightWall, rightStart);
        MoveWall(backWall, backStart);
    }

    void MoveWall(Transform wall, Vector3 startPosition)
    {
        Vector3 direction = centerDirection(startPosition);
        Vector3 target = startPosition + direction * moveDistance;

        Vector3 oldPosition = wall.position;

        wall.position = Vector3.MoveTowards(
            wall.position,
            target,
            speed * Time.deltaTime
        );

    }
    Vector3 centerDirection(Vector3 startPosition)
    {
        Vector3 direction = centerPoint.position - startPosition;
        direction.y = 0;

        return direction.normalized;
    }

    public void CloseRoom()
    {
        closing = true;
    }
}
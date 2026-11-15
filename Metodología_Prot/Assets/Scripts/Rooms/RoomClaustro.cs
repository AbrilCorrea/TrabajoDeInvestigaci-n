using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClaustro : MonoBehaviour
{
    [SerializeField] private Transform[] walls;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float speed = 1f;

    private bool closing = false;

    void Update()
    {
        if (!closing) return;

        foreach (Transform wall in walls)
        {
            wall.position = Vector3.MoveTowards(
                wall.position,
                centerPoint.position,
                speed * Time.deltaTime
            );
        }
    }

    public void CloseRoom()
    {
        closing = true;
    }
}
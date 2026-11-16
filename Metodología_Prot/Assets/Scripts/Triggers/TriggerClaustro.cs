using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClaustro : MonoBehaviour
{
    public InteractiveDoor door;
    public KeyPad4 keypad;
    public RoomClaustro roomClaustro;
    public DoorsElevator elevatorDoors;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        keypad.canUseKeypad = true;

        door.CloseDoorExternally();
        door.canInteract = false;

        if (elevatorDoors != null)
        {
            elevatorDoors.CloseDoors();
        }
      

        roomClaustro.CloseRoom();

        gameObject.SetActive(false);
    }
} 
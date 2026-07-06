using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoors : MonoBehaviour
{
    public InteractiveDoor door;
    public KeyPad2 keypad;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        keypad.canUseKeypad = true;

        door.CloseDoorExternally();
        door.canInteract = false;

        gameObject.SetActive(false);
    }
}

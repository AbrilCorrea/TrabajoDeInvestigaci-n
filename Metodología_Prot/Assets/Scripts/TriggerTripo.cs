using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTripo : MonoBehaviour
{

    public InteractiveDoor door;
    public KeyPad3 keypad;

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

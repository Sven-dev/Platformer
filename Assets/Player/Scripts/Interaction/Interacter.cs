using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    public bool Interacting;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Interacting && Controller.InteractButton.Pressed)
        {
            //Get interaction data
            InteractionController imanager = collision.GetComponent<InteractionController>();

            //Start interacting
            imanager.Loop(this);
        }
    }
}
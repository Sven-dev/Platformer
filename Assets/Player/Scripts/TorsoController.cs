using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoController : MonoBehaviour
{
    [SerializeField] private Animator TorsoAnimator;

    private Vector2 Input = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.Moving)
        {
            TorsoAnimator.SetFloat("Speed", 0);
            return;
        }

        //Get the correct input (aiming if the right stick is being moved, movement if not)
        if (Controller.Aiming != Vector2.zero)
        {
            // Player is moving the right stick
            Input = Controller.Aiming;
        }
        else if (Controller.Movement != Vector2.zero)
        {
            //Player is moving the left stick
            Input = Controller.Movement;
        }

        //Animation
        TorsoAnimator.SetFloat("Speed", Controller.Movement.sqrMagnitude);
        TorsoAnimator.speed = Mathf.Clamp01(Input.sqrMagnitude + 0.5f);
        if (Input.sqrMagnitude > 0.01f)
        {
            //Only update the direction when the character is moving, so it keeps its direction for the idle animation
            TorsoAnimator.SetFloat("Horizontal", Input.x);
            TorsoAnimator.SetFloat("Vertical", Input.y);
        }
    }
}
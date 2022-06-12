using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player movement
/// </summary>
public class LegController : MonoBehaviour
{
    [SerializeField] private Animator LegAnimator;

    private Vector2 Input = Vector2.zero;

    private void Update()
    {
        if (!Player.Instance.Moving)
        {
            LegAnimator.SetFloat("Speed", 0);
            return;
        }

        //Get the movement from the controller
        Input = Controller.Movement;

        //Animation
        LegAnimator.SetFloat("Speed", Input.sqrMagnitude);
        LegAnimator.speed = Mathf.Clamp01(Input.sqrMagnitude + 0.5f);
        if (Input.sqrMagnitude > 0.01f)
        {
            //Only update the direction when the character is moving, so it keeps its direction for the idle animation
            LegAnimator.SetFloat("Horizontal", Input.x);
            LegAnimator.SetFloat("Vertical", Input.y);
        }
    }
}
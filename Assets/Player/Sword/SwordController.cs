using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordState
{
    Thrown,
    Aiming,
    Holding
}

public class SwordController : MonoBehaviour
{
    public SwordState SwordState = SwordState.Holding;
    [Space]
    public Transform Pivot;
    [SerializeField] private Animator SwordAnimator;

    private float Rotation;
    private float DeltaRotation;

    private void Update()
    {
        /*if (!Player.Instance.Moving)
        {
            SwordAnimator.SetFloat("Speed", 0);
            return;
        }*/

        //Get the correct input (aiming if the right stick is being moved, movement if not)
        Vector2 input = Vector2.zero;
        if (Controller.Aiming != Vector2.zero)
        {
            // Player is moving the right stick
            input = Controller.Aiming;
        }
        else if (Controller.Movement != Vector2.zero)
        {
            //Player is moving the left stick
            input = Controller.Movement;
        }

        //Aim the sword
        RotateSword(input);

        //Sword animation
        DeltaRotation = Rotation;
        Rotation = transform.rotation.eulerAngles.z;
        SwordAnimator.SetFloat("Speed", Mathf.DeltaAngle(Rotation, DeltaRotation) / 100);
    }

    /// <summary>
    /// Aims the sword in the right direction
    /// </summary>
    /// <param name="direction"></param>
    public void RotateSword(Vector2 direction)
    {
        //If the player is aiming, point in the opposite direction
        if (SwordState == SwordState.Aiming)
        {
            direction = -direction;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Pivot.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 15);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordThrowingController : MonoBehaviour
{
    [SerializeField] private SwordController SwordController;
    [SerializeField] private GameObject Sword;
    [SerializeField] private Animator TorsoAnimator;
    [Space]
    [SerializeField] private SwordProjectile SwordProjectilePrefab;
    [SerializeField] private SpriteRenderer AimGizmo;

    private bool SwordAvailable = true;
    private bool Aiming = false;

    private void Update()
    {
        //If the player doesn't have a sword or is already aiming, don't do anything
        if (!SwordAvailable || Aiming)
        {
            return;
        }

        //If the player just pressed the throw button, start aiming
        if (Controller.ThrowButton.Pressed)
        {
            StartCoroutine(_Aiming());
        }
    }

    public void PickupSword()
    {
        Sword.SetActive(true);
        SwordController.SwordState = SwordState.Holding;
        SwordAvailable = true;
    }

    private IEnumerator _Aiming()
    {
        SwordController.SwordState = SwordState.Aiming;
        TorsoAnimator.SetTrigger("Aim");
        AimGizmo.enabled = true;
        Aiming = true;      

        while (Controller.ThrowButton.Held)
        {
            //Wait until the player lets go of the button
            yield return null;
        }

        SwordController.SwordState = SwordState.Thrown;
        TorsoAnimator.SetTrigger("Throw");
        AimGizmo.enabled = false;
        Aiming = false;               

        SwordAvailable = false;
        ThrowSword();       
    }

    private void ThrowSword()
    {
        Sword.SetActive(false);

        //Get the correct input (aiming if the right stick is being moved, movement if not)
        Vector3 direction = Vector3.zero;
        if (Controller.Aiming != Vector2.zero)
        {
            // Player is moving the right stick
            direction = Controller.Aiming;
        }
        else if (Controller.Movement != Vector2.zero)
        {
            //Player is moving the left stick
            direction = Controller.Movement;
        }

        //Calculate the angle to rotate the sword in
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //Instantiate sword projectile
        Instantiate(SwordProjectilePrefab, Player.Instance.transform.position, q);
    }
}
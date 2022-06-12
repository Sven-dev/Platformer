using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public static SwordProjectile Instance;

    [HideInInspector] public bool Stuck = false;

    [SerializeField] private float Speed = 1;
    [Space]
    [SerializeField] private Animator Animator;

    [SerializeField] private LayerMask WallMask;
    [SerializeField] private LayerMask PlayerMask;

    private void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        transform.localPosition += transform.up * Speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hitting a wall
        if (WallMask == (WallMask | (1 << collision.gameObject.layer)))
        {
            Animator.SetBool("Stuck", true);
            Stuck = true;
            Speed = 0;
        }
        //Picking up the sword
        else if (PlayerMask == (PlayerMask | (1 << collision.gameObject.layer)))
        {
            //Check if the sword is stuck in the wall
            if (Stuck)
            {
                //Have the player pick up the sword
                ReturnToPlayer();
            }
        }
        else //Error handling
        {
            Debug.LogWarning("Warning: collision on " + gameObject.name + " between " +
                LayerMask.LayerToName(gameObject.layer) + " and " +
                LayerMask.LayerToName(collision.gameObject.layer) + " doesn't get handled (is it supposed to collide?)");
        }
    }

    /// <summary>
    /// Returns the sword to the player
    /// </summary>
    public void ReturnToPlayer()
    {
        Player.Instance.SwordThrowingController.PickupSword();
        Instance = null;
        Destroy(gameObject);      
    }
}
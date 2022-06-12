using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D Hitbox;

    [HideInInspector] public Vector3 DeltaHitbox = Vector3.zero;

    private void LateUpdate()
    {
        DeltaHitbox = Hitbox.transform.position;
    }
}

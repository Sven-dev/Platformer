using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform Target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position + Vector3.back * 10;
    }
}

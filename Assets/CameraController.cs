using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 Bounds;
    [SerializeField] private float Speed = 1f;
    [Space]
    [SerializeField] private Transform Target;
    [SerializeField] private Transform Cam;

    private Vector2 CornerDistance;

    private void Start()
    {
        SetCornerDistance();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        ClampBounds();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    void UpdateCameraPosition()
    {
        if (Target != null)
        {
            Cam.position = Vector2.Lerp(transform.position, Target.transform.position, Speed);
        }
    }

    //Calculates the x and y distance between the center and the corners of the camera 
    private void SetCornerDistance()
    {
        CornerDistance = new Vector2(13.3125f, 7.5f);
    }

    //Ensures the camera stays within the bounds of the map
    private void ClampBounds()
    {
        float xmin = -Bounds.x + CornerDistance.x;
        float xmax = Bounds.x - CornerDistance.x;
        float x = transform.position.x;
        if (xmin < xmax)
        {
            x = Mathf.Clamp(
                Cam.position.x,
                transform.position.x + xmin,
                transform.position.x + xmax);
        }

        float ymin = -Bounds.y + CornerDistance.y;
        float ymax = Bounds.y - CornerDistance.y;
        float y = transform.position.y;
        if (ymin < ymax)
        {
            y = Mathf.Clamp(
                Cam.position.y,
                transform.position.y + ymin,
                transform.position.y + ymax);
        }

        //Clamp the corners of the camera
        Cam.position = new Vector3(x, y, -10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Bounds * 2);

        Vector2 camspace = Bounds * 2 - CornerDistance;
        if (camspace.x < 0 || camspace.y < 0)
        {
            Gizmos.color = Color.red;
        }

        Vector2 temp = Bounds;
        temp.x -= CornerDistance.x;
        temp.y -= CornerDistance.y;
        Gizmos.DrawWireCube(transform.position, temp * 2);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The character to follow
    public Vector3 offset; // Offset between the camera and the character
    public float smoothSpeed = 0.125f; // Smooth speed of the camera

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Optional: Rotate the camera to look at the target
            transform.LookAt(target);
        }
    }
}

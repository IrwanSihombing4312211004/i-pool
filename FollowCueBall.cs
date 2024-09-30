using UnityEngine;

public class FollowCueBall : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform cueBall;      // Reference to the cue ball's transform

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0, 5, -10); // Adjusted for better height

    [Header("Smooth Settings")]
    public float smoothSpeed = 5f; // Speed of the camera's smoothing

    [Header("Height Constraints")]
    public float minHeight = 3f;   // Minimum Y position

    void LateUpdate()
    {
        if (cueBall == null)
        {
            Debug.LogWarning("FollowCueBall: Cue ball not assigned.");
            return;
        }

        // Desired position based on the cue ball's position and offset
        Vector3 desiredPosition = cueBall.position + offset;

        // Clamp the Y position to prevent clipping into the table
        desiredPosition.y = Mathf.Max(desiredPosition.y, minHeight);

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Optional: Make the camera look at the cue ball
        transform.LookAt(cueBall);
    }
}

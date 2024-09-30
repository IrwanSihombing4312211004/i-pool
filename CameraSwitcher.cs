using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;        // Reference to the main camera
    public Camera topDownCamera;     // Reference to the top-down camera
    public Camera cueBallCamera;     // Reference to the cue ball camera

    private int activeCameraIndex = 0; // 0: Main, 1: Top-Down, 2: Cue Ball
    private Camera[] cameras;          // Array to hold all cameras

    void Start()
    {
        // Initialize the cameras array
        cameras = new Camera[] { mainCamera, topDownCamera, cueBallCamera };

        // Ensure only the main camera is active at the start
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != null)
            {
                cameras[i].enabled = (i == activeCameraIndex);
            }
            else
            {
                Debug.LogError("One or more cameras are not assigned in the Inspector.");
            }
        }

    }

    void Update()
    {
        // Listen for the 'C' key to switch cameras
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {   
        {
            ToggleCamera();
        }
    }

    void ToggleCamera()
    {
        // Disable the current camera
        if (activeCameraIndex >= 0 && activeCameraIndex < cameras.Length)
        {
            cameras[activeCameraIndex].enabled = false;
        }

        // Update the active camera index
        activeCameraIndex = (activeCameraIndex + 1) % cameras.Length;

        // Enable the new active camera
        if (activeCameraIndex >= 0 && activeCameraIndex < cameras.Length)
        {
            cameras[activeCameraIndex].enabled = true;
        }
    }
}

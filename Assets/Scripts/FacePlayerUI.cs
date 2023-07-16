using UnityEngine;

public class FacePlayerUI : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Transform target;  // Reference to the NPC's transform

    private void LateUpdate()
    {
        // Check if the target or camera reference is null
        if (target == null || mainCamera == null)
            return;

        // Get the camera transform
        Transform cameraTransform = mainCamera.transform;

        // Make the UI bubble face the camera
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);

        // Match the position of the UI bubble to the NPC's position
        transform.position = target.position;
    }
}

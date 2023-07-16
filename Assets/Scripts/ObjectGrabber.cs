using UnityEngine;
using System.Collections;

public class ObjectGrabber : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // The key to trigger the interaction
    [SerializeField] private GameObject objectToDisable; // Reference to the object to disable
    [SerializeField] private GameObject player; // Reference to the player object
    [SerializeField] private float interactionDistance = 2f; // Customizable distance for interaction
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private Canvas canvasToEnable; // Reference to the Canvas component to enable
    [SerializeField] private GameObject pressKeyUI;
    [SerializeField] private GameObject pressKeyUIParent;
    [SerializeField] private MonoBehaviour cameraController;

    private bool canInteract = false; // Flag indicating if the player can interact

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && canInteract)
        {
            // Trigger the animator trigger
            animator.SetTrigger("Interaction");
            // Disable the referenced script
            this.GetComponent<CharacterControl>().enabled = false;
            cameraController.enabled = false;
            //Unlock and show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // Start the coroutine to disable the object after 1.5 seconds
            StartCoroutine(DisableObjectAfterDelay(1.2f));
            pressKeyUIParent.SetActive(false);
        }
        if (canInteract) {
            pressKeyUI.SetActive(true);
        }
        else {
            pressKeyUI.SetActive(false);
        }
    }

    private IEnumerator DisableObjectAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the object
        //objectToDisable.SetActive(false);

        // Wait for another 1.5 seconds
        yield return new WaitForSeconds(delay);

        // Enable the canvas
        canvasToEnable.enabled = true;
        enabled = false;
    }

    private void FixedUpdate()
    {
        // Calculate the distance between the player and the object
        float distance = Vector3.Distance(player.transform.position, objectToDisable.transform.position);

        // Check if the player is within the interaction distance
        canInteract = distance <= interactionDistance;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the interaction distance in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(objectToDisable.transform.position, interactionDistance);
    }
}
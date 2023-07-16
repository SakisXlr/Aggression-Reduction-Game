using UnityEngine;


public class NPC : MonoBehaviour
{
    public Transform player;
    public float distanceToKeep = 5f;
    public float walkingSpeed = 2f;
    public Animator animator;
    public Canvas canvas2; // Reference to the Canvas component to enable

    public MonoBehaviour gun;
    public MonoBehaviour playerMoveScript;
    public MonoBehaviour cameraScript;

    private bool isWalking = false;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the NPC to the player
            Vector3 direction = player.position - transform.position;
            direction.y = 0f; // Ignore any vertical distance

            // Calculate the desired position for the NPC to maintain the desired distance from the player
            Vector3 targetPosition = player.position - direction.normalized * distanceToKeep;

            // Move the NPC towards the target position at a walking speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkingSpeed * Time.deltaTime);

            // Rotate the NPC to look at the player's position
            transform.LookAt(player);

            // Check if the NPC is walking
            if (!isWalking && Vector3.Distance(transform.position, targetPosition) > distanceToKeep)
            {
                isWalking = true;
                animator.SetBool("IsWalking", true);
            }
            // Check if the NPC has reached the desired distance
            else if (isWalking && Vector3.Distance(transform.position, targetPosition) <= distanceToKeep)
            {
                isWalking = false;
                animator.SetBool("IsWalking", false);
                gun.enabled = true;
                StartCoroutine(DisableGunAfterDelay(8f));
            }
        }
    }
    private System.Collections.IEnumerator DisableGunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gun.enabled = false;
        canvas2.enabled = true;
        playerMoveScript.enabled = false;
        cameraScript.enabled = false;
        //Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

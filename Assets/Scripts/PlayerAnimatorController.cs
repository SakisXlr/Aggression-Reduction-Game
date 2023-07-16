using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if 'W' key or up arrow is pressed
        bool isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        // Set the "Running" parameter in the animator
        animator.SetBool("Running", isRunning);
    }
}

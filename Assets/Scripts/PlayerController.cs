using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    public GameOverScript gameOverScript;
    public float jumpForce = 7f;
    public float slideDuration = 0.5f;

    private Animator anim;
    private Rigidbody2D rb;
    
    private PlayerColliderHandler colliderHandler;

    private bool isGrounded = true;
    private bool isSliding = false;

    private Vector2 touchStart;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // enables EnhancedTouch system
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliderHandler = GetComponent<PlayerColliderHandler>();

    }

    void Update()
    {
        // ---- PC Controls ----
        if (KeybindManager.Instance.GetKeyDown(KeybindManager.Instance.KeyJump) && isGrounded)
        {
            Jump();
        }

        if (KeybindManager.Instance.GetKeyDown(KeybindManager.Instance.KeySlide) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }

        // ---- Mobile Controls (EnhancedTouch) ----
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
        {
            var activeTouch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];

            if (activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                touchStart = activeTouch.screenPosition;
            }
            else if (activeTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                Vector2 touchEnd = activeTouch.screenPosition;
                Vector2 swipe = touchEnd - touchStart;

                if (swipe.magnitude > 50) // Minimum swipe distance
                {
                    Vector2 delta = touchEnd - touchStart;

                    if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x)) // Vertical swipe
                    {
                        if (delta.y > 0 && isGrounded) // Swipe up
                        {
                            Jump();
                        }
                        else if (delta.y < 0 && isGrounded && !isSliding) // Swipe down
                        {
                            StartCoroutine(Slide());
                        }
                    }
                }
            }
        }
    }

    void Jump()
    {
        if (!isGrounded) return;

        colliderHandler.SwitchToJumpCollider(); // Switch to jumping collider
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        anim.SetTrigger("Jump");
        
    }

    public void EndJump()
    {
        colliderHandler.SwitchToDefaultCollider(); // Switch back to default collider
        anim.SetBool("isWalking", true);
    }

    System.Collections.IEnumerator Slide()
    {
        isSliding = true;
        colliderHandler.SwitchToSlideCollider(); // Switch to sliding collider
        anim.SetTrigger("Slide");

        yield return new WaitForSeconds(slideDuration);

        isSliding = false;
        colliderHandler.SwitchToDefaultCollider(); // Switch back to default collider
    }

    public void EndSlide()
    {
        anim.SetBool("isWalking", true);
        colliderHandler.SwitchToDefaultCollider(); // Switch back to default collider
        isSliding = false; // safety in case coroutine finishes early
    }

    public void EndWalk()
    {
        anim.SetBool("isWalking", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isWalking", true);
            colliderHandler.SwitchToDefaultCollider(); // Switch back to default collider
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Disable all player colliders so he can fall through the ground
            Collider2D[] allColliders = GetComponents<Collider2D>();
            foreach (Collider2D col in allColliders)
            {
                col.enabled = false;
            }

            // Apply a downward velocity to the Rigidbody
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -10f);

            // Stop the player controller script
            this.enabled = false;

            if (gameOverScript != null)
            {
                gameOverScript.ShowGameOver();
            }
        }
    }

}
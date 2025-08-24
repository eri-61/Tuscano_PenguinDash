using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    public GameOverScript gameOverScript;
    public float jumpForce = 7f;
    public float slideDuration = 0.5f;

    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;

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
    void Awake()
    {
        // Get references to other components in Awake() to ensure they're available
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        anim.SetTrigger("Jump");
        
    }

    public void EndJump()
    {
        anim.SetBool("isWalking", true);
    }

    System.Collections.IEnumerator Slide()
    {
        isSliding = true;
        anim.SetTrigger("Slide");

        yield return new WaitForSeconds(slideDuration);

        isSliding = false;
    }

    public void EndSlide()
    {
        anim.SetBool("isWalking", true);
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

        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -10f);
            this.enabled = false;

            if (gameOverScript != null)
            {
                gameOverScript.ShowGameOver();
            }
        }
    }

}
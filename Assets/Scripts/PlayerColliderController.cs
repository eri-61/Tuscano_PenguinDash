using UnityEditor.Build;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    public Collider2D walkingCollider;
    public Collider2D slidingCollider;
    public Collider2D jumpingCollider;
    void Start()
    {
        // Ensure only the default collider is active at the start
        walkingCollider.enabled = true;
        slidingCollider.enabled = false;
        jumpingCollider.enabled = false;
    }

    // This function is called by an Animation Event at the start of the slide animation
    public void SwitchToSlideCollider()
    {
        walkingCollider.enabled = false;
        jumpingCollider.enabled = false;
        slidingCollider.enabled = true;
    }
    public void SwitchToJumpCollider()
    {
        walkingCollider.enabled = false;
        jumpingCollider.enabled = true;
        slidingCollider.enabled = false;
    }

    // This function is called by an Animation Event at the end of the slide animation
    public void SwitchToDefaultCollider()
    {
        walkingCollider.enabled = true;
        slidingCollider.enabled = false;
        jumpingCollider.enabled = false;
    }
}
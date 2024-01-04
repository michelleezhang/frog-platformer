using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.5f;
    private Rigidbody2D rigidBody;
    private bool isGrounded;
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, 0);
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal, 0);
        rigidBody.velocity = new Vector2(movement.x * speed, rigidBody.velocity.y);
   
        
        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) // Only jump if we start off on the ground
        {
            audioSources[0].PlayOneShot(audioSources[0].clip);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        // Clamp position to screen boundaries
        ClampPosition();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (gameObject.activeInHierarchy) // Make sure that game object is still active
            {
                transform.parent = collision.transform; // Effectively sticks player on top of ground
            }
        }

        else if (collision.gameObject.CompareTag("Finish"))
        {
            // Play sound
            audioSources[1].PlayOneShot(audioSources[1].clip);

            // If we collected all the fruit, then we win!
            ScoreKeeper.TriggerWin(collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            if (gameObject.activeInHierarchy) 
            {
                transform.parent = null;
            }
    
        }
    }

    void FixedUpdate()
    {
        // Freeze rotation
        rigidBody.rotation = 0f;
    }

    void ClampPosition()
    {
        // Get screen boundaries in world space
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);

        // Clamp position to screen boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min.x, max.x);
        transform.position = clampedPosition;
    }

}

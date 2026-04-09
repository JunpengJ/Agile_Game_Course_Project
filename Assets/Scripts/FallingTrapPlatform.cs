using UnityEngine;

public class FallingTrapPlatform : MonoBehaviour
{
    public float fallDelay = 0.3f;
    public float destroyAfterFall = 3f;
    public LayerMask playerLayer;

    private Rigidbody2D  rb;
    private Collider2D platformCollider;
    private bool hasFallen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platformCollider = GetComponent<Collider2D>();
        platformCollider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = true;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        if(hasFallen) return;
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            hasFallen = true;
            Invoke(nameof(StartFalling), fallDelay);
        }
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (platformCollider != null) 
        {
            platformCollider.isTrigger = false;
            platformCollider.enabled = true;
        };
        Destroy(gameObject, destroyAfterFall);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D triggered with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player is here");
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                Debug.Log("Calling Die()");
                player.Die();
            }
            else
            {
                Debug.Log("PlayerMovement component not found on " + collision.gameObject.name);
            }
        }
    }
}

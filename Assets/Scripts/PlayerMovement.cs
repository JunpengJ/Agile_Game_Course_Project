using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public float speed = 5f;
    public float jumpForce = 10f;
    private float playerHalfWidth;
    public float deathY = -5f;
    public float headCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public float headBounceForce = -5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        startPosition = transform.position;
        if (groundLayer == 0) groundLayer = ~0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        Bounds bounds = tilemap.GetComponent<TilemapRenderer>().bounds;
        pos.x = Mathf.Clamp(pos.x, bounds.min.x + playerHalfWidth, bounds.max.x - playerHalfWidth);
        transform.position = pos;
    }

    void Movement()
    {
        float move = 0f;
        if(Keyboard.current.aKey.isPressed) move = -1f;
        if(Keyboard.current.dKey.isPressed) move = 1f;

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    public void Die()
    {
        if (GameManager.Instance != null) GameManager.Instance.AddDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
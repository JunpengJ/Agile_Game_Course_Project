using UnityEngine;

public class HiddenPlatform : MonoBehaviour
{
    public float appearDelay = 0f;
    public bool disappearAfterUse = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;
    private bool isActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (platformCollider != null)
            platformCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        // 判断是否从下方碰撞
        if (IsCollisionFromBelow(collision))
        {
            isActive = true;
            if (appearDelay > 0)
                Invoke(nameof(ShowPlatform), appearDelay);
            else
                ShowPlatform();
        }
    }

    private bool IsCollisionFromBelow(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        return contact.normal.y > 0.5f;
    }

    void ShowPlatform()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;
        if (disappearAfterUse)
        {
            Invoke(nameof(HidePlatform), 2f);
        }
    }

    void HidePlatform()
    {
        SetVisible(false);
        isActive = false;
    }

    void SetVisible(bool visible)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = visible;
    }
}

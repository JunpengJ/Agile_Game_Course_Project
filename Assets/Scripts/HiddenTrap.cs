using UnityEngine;
using System.Collections;

public class HiddenTrap : MonoBehaviour
{
    public float appearDelay = 0f;
    public float deathDelay = 0.5f; 
    public bool disableColliderOnTrigger = false;
    public bool destroyOnTrigger = true; 
    private SpriteRenderer spriteRenderer;
    private Collider2D trapCollider;
    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trapCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
        if (trapCollider != null)
            trapCollider.enabled = true;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered) return;
        if (!collision.gameObject.CompareTag("Player")) return;

         PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Die();
            triggered = true;
        }

        if (destroyOnTrigger)
        {
            Destroy(gameObject);
        }
        else if (disableColliderOnTrigger)
        {
            if (trapCollider != null) trapCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(TriggerTrap(other.gameObject));
    }

     private IEnumerator TriggerTrap(GameObject playerObj)
    {
        triggered = true;

        if (appearDelay > 0)
            yield return new WaitForSeconds(appearDelay);

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        yield return new WaitForSeconds(deathDelay);

        PlayerMovement player = playerObj.GetComponent<PlayerMovement>();
        if (player != null)
            player.Die();

        if (destroyOnTrigger)
            Destroy(gameObject);
        else if (disableColliderOnTrigger && trapCollider != null)
            trapCollider.enabled = false;
    }
}

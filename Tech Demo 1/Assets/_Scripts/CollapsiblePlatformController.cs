using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collapsible Platform Controller:
/// In-charge of the following:
/// 1. Detecting when the player makes contact with the player to begin the 'collapsing' stage of the platform
/// 2. Collapse over a defined duration of time, visually this is done by changing the opacity % using the 
///    remaining duration from the total duration expressed as a percentage
/// </summary>
public class CollapsiblePlatformController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Collapsible Settings:")]
    [Tooltip("Duration in seconds.")]
    [SerializeField] private float maxCollapseDuration;
    private float currentCollapseDuration;
    private bool playerStood = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentCollapseDuration = maxCollapseDuration;
    }

    private void Update()
    {
        Collapse();
    }

    private void Collapse()
    {
        if (playerStood)
        {
            // INFO: Given that the collapsing duration has run out we disable the parent of the gameObject
            // which in turn disables all the child objects
            if (currentCollapseDuration <= 0.0f)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }

            currentCollapseDuration -= Time.deltaTime;

            float opacityPercentage = currentCollapseDuration / maxCollapseDuration;

            // INFO: Number rounded to 2 decimal places for a smoother change in opacity levels
            System.Math.Round(opacityPercentage, 2);

            // INFO: RGB remains the same to retain the sprites colors, however the opacity level slowly decreases eventually becoming transparent
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacityPercentage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStood = true;
        }
    }
}

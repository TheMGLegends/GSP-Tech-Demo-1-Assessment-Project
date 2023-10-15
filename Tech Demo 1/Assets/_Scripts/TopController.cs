using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Top Controller:
/// - In-charge of controlling the following actions:
///   1. Allowing the player to descend the ladder when the user presses the
///   down key (S) whilst the player is located above the ladder
/// </summary>
public class TopController : MonoBehaviour
{
    [Header("Ladder Platform Reference:")]
    [SerializeField] private BoxCollider2D ladderPlatformCollider;

    private bool climbDown;

    private void Update()
    {
        if (climbDown && Input.GetKeyDown(KeyCode.S))
        {
            ladderPlatformCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            climbDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            climbDown = false;
        }
    }
}

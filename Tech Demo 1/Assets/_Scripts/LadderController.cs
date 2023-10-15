using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ladder Controller:
/// - In-charge of controlling the following actions:
///   1. Switching the ladder platform located at the top of the platform 
///   from a passable to impassable state so that the player can go through
///   it when climbing up, but can't fall through it when reaching the top
/// </summary>
public class LadderController : MonoBehaviour
{
    [Header("Ladder Platform Collider:")]
    [SerializeField] private BoxCollider2D ladderPlatform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ladderPlatform.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ladderPlatform.isTrigger = false;
        }
    }
}

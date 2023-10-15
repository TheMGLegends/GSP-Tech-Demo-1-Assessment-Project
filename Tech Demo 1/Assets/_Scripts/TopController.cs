using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

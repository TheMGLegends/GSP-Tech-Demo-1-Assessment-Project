using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moving Platform Controller:
/// In-charge of the following:
/// 1. Moving itself (platform) between a number of waypoints (Default = 2)
/// 2. Setting the parent of any GameObject with the 'Player' tag when it collides with its trigger collider
/// </summary>
public class MovingPlatformController : MonoBehaviour
{
    private int waypointIndex = 0;

    [Header("Move Settings:")]
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private float movementSpeed;

    private void Update()
    {
        MoveBetweenWaypoints();
    }

    private void MoveBetweenWaypoints()
    {
        // INFO: Moves from its current position towards a position held at waypointIndex in the wayPoints list
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[waypointIndex].position, movementSpeed * Time.deltaTime);

        // INFO: Given that the distance between itself and the destination object is less than some amount
        if (Mathf.Abs((transform.position - wayPoints[waypointIndex].position).magnitude) < 0.1f)
        {
            // INFO: Given that the index is equal to the size of the list we know we've reached our last waypoint
            if (waypointIndex == wayPoints.Count - 1)
            {
                // INFO: Hence we reset it to 0 to move to the very first platform, creating a looping effect
                waypointIndex = 0;
            }
            else
            {
                // INFO: Otherwise the index will be increased so that the platform can move to the next waypoint in the list
                waypointIndex++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // INFO: Parents the player when they are on the platform
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // INFO: De-parents the player when they jump off the platform
            collision.gameObject.transform.parent = null;
        }
    }
}

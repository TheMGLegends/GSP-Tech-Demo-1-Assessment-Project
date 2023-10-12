using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Boomerang Controller:
/// - In-charge of controlling the following actions:
///   1. Moving Away from the player when thrown
///   2. Returning to the player once it has travelled a certain distance
/// </summary>
public class BoomerangController : MonoBehaviour
{
    private GameObject playerObject;
    private Transform childObject;

    [Header("Boomerang Settings:")]
    [SerializeField] private float throwingDistance;
    [SerializeField] private float throwingSpeed;
    [SerializeField] private float rotationAngle;
    private Vector3 endingPosition;
    private bool isAway = true;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        childObject = gameObject.transform.GetChild(0);

        // INFO: If facing left (Local Scale X = -1), hence will transfrom distance and angle to go in the left direction
        throwingDistance *= transform.localScale.x;
        rotationAngle *= transform.localScale.x;

        endingPosition = new (transform.position.x + throwingDistance, transform.position.y, transform.position.z);
    }

    void Update()
    {
        MoveBoomerang();
    }

    private void MoveBoomerang()
    {
        if (isAway)
        {
            childObject.Rotate(new Vector3(0, 0, -rotationAngle));
            transform.position = Vector2.MoveTowards(transform.position, endingPosition, throwingSpeed * Time.deltaTime);

            // INFO: Once the given distance has been reached it will start the returning stage
            if (transform.position == endingPosition)
            {
                isAway = false;
            }
        }
        else
        {
            childObject.Rotate(new Vector3(0, 0, rotationAngle));
            transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, throwingSpeed * Time.deltaTime);

            // INFO: Once the boomerang has returned to the player it will be destroyed, so another boomerang can be thrown
            if (transform.position == playerObject.transform.position)
            {
                Destroy(gameObject);
                playerObject.GetComponent<PlayerController>().ResetBoomerang(isAway);
            }
        }
    }
}

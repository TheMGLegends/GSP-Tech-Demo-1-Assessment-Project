using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follow Object:
/// In-charge of following:
/// 1. Following the position of the given object
/// </summary>
public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectPosition;

    private void Update()
    {
        Vector2 currentPos = new(transform.position.x, transform.position.y);
        Vector2 futurePos = new(objectPosition.position.x, objectPosition.position.y); 

        // INFO: Only ever updates if the following object has moved, done to prevent unecessary 
        // alterations to transform position when the position hasn't even changed
        if (currentPos != futurePos)
        {
            transform.position = new Vector3(futurePos.x, futurePos.y, transform.position.z);
        }
    }
}

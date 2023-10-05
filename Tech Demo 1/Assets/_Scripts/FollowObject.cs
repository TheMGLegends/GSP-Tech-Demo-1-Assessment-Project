using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follow Object:
/// In-charge of following the position of a given object
/// </summary>
public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectPosition;

    private void Update()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 futurePos = new Vector2(objectPosition.position.x, objectPosition.position.y); 

        if (currentPos != futurePos)
        {
            transform.position = new Vector3(futurePos.x, futurePos.y, transform.position.z);
        }
    }
}

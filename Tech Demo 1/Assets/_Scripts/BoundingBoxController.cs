using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bounding Box Controller:
/// In-charge of the following:
/// 1. Passing along its bounds to the camera controller script when the player object steps into it
/// </summary>
public class BoundingBoxController : MonoBehaviour
{
    [Header("Camera Script:")]
    [SerializeField] private CameraController cameraController;

    private Bounds boxBounds;

    private void Start()
    {
        boxBounds = GetComponent<BoxCollider2D>().bounds;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // INFO: Works out the top right vector (maxX, maxY) and bottom left vector (minX, minY) of the BoxCollider2D
            Vector2 maxBounds = new(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
            Vector2 minBounds = new(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y);

            cameraController.ChangeBounds(maxBounds, minBounds);
        }
    }
}

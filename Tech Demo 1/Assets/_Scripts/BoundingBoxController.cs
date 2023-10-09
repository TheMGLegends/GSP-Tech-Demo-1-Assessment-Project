using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector2 maxBounds = new(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
            Vector2 minBounds = new(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y);

            cameraController.ChangeBounds(maxBounds, minBounds);
        }
    }
}

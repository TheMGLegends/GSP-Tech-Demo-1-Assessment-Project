using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Section Transition Settings:")]
    [SerializeField] private float lerpDuration;

    [Header("Player Object:")]
    [SerializeField] private GameObject playerObject;

    private Camera cam;
    private Vector2 maxBounds;
    private Vector2 minBounds;

    private float halfCameraWidth;
    private float halfCameraHeight;

    private void Start()
    {
        cam = GetComponent<Camera>();

        halfCameraWidth = cam.orthographicSize * Camera.main.aspect;
        halfCameraHeight = cam.orthographicSize;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(Mathf.Clamp(playerObject.transform.position.x, minBounds.x + halfCameraWidth, maxBounds.x - halfCameraWidth),
                                         Mathf.Clamp(playerObject.transform.position.y, minBounds.y + halfCameraHeight, maxBounds.y - halfCameraHeight), transform.position.z);
    }

    public void ChangeBounds(Vector2 maximumBounds, Vector2 minimumBounds)
    {
        maxBounds = maximumBounds;
        minBounds = minimumBounds;
    }
}

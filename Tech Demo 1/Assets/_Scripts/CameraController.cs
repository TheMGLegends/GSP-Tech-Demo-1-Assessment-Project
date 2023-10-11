using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Camera Controller:
/// In-charge of the following:
/// 1. Following the player, whilst adhering to staying within the current bounding box,
///    hence camera will stop moving once player gets too close to the edge of the bounding box
/// 2. ChangeBounds allows for other bounding boxes that get entered by the player to pass their
///    bounding areas so that the camera can transition smoothly to the next 'section'
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Section Transition Settings:")]
    [SerializeField] private float cameraSpeed;

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
        // INFO: Camera follows the players position, so long as the players position remains within the defined x and y constraints
        Vector3 newPosition = new(Mathf.Clamp(playerObject.transform.position.x, minBounds.x + halfCameraWidth, maxBounds.x - halfCameraWidth),
                                  Mathf.Clamp(playerObject.transform.position.y, minBounds.y + halfCameraHeight, maxBounds.y - halfCameraHeight), transform.position.z);

        // INFO: MoveTowards is used to slow down the camera movement in order to make sure the camera doesn't just snap to the new section,
        // instead it progressively shows the player based on the camera speed defined by the user
        transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed);
    }

    public void ChangeBounds(Vector2 maximumBounds, Vector2 minimumBounds)
    {
        maxBounds = maximumBounds;
        minBounds = minimumBounds;
    }
}

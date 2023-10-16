using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBManager : MonoBehaviour
{
    [SerializeField] private GameObject boundingBox;
    [SerializeField] private Transform player;
    private BoxCollider2D managerBoxCollider;

    private void Start()
    {
        managerBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        IsPlayerInside();
    }

    private void IsPlayerInside()
    {
        if (player.position.x > managerBoxCollider.bounds.min.x && player.position.x < managerBoxCollider.bounds.max.x
                    && player.position.y > managerBoxCollider.bounds.min.y && player.position.y < managerBoxCollider.bounds.max.y)
        {
            boundingBox.SetActive(true);
        }
        else
        {
            boundingBox.SetActive(false);
        }
    }
}

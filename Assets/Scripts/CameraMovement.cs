using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minVertical = 0f;
    [SerializeField] private float minHorizontal = 0f;
    [SerializeField] private float maxVertical = float.PositiveInfinity;
    [SerializeField] private float maxHorizontal = float.PositiveInfinity;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private GameObject toFollow;

    private Vector2 cameraVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = toFollow.transform.position;
        SnapToConstraints();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = Vector2.SmoothDamp(transform.position, toFollow.transform.position, ref cameraVelocity, followSpeed);
        SnapToConstraints();
    }

    private void SnapToConstraints()
    {
        transform.position = new Vector3(
            Mathf.Min(maxHorizontal, Mathf.Max(minHorizontal, transform.position.x)),
            Mathf.Min(maxVertical, Mathf.Max(minVertical, transform.position.y)),
            -10);
    }
}

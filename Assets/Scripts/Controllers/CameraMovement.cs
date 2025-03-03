using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minVertical = 0f;
    [SerializeField] private float minHorizontal = 0f;
    [SerializeField] private float maxVertical = float.PositiveInfinity;
    [SerializeField] private float maxHorizontal = float.PositiveInfinity;
    
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private GameObject toFollow;

    [SerializeField] private float maxFollowDistanceHorizontal = 25f;
    [SerializeField] private float maxFollowDistanceVertical = 25f;
    [SerializeField] private float snapSpeed = 10f;

    [SerializeField] private float lookahead = 3f;

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
        float diffX = Mathf.Abs(transform.position.x - toFollow.transform.position.x);
        float diffY = Mathf.Abs(transform.position.y - toFollow.transform.position.y);
        Vector2 vel = toFollow.GetComponent<Rigidbody2D>().linearVelocity;

        if (diffX > maxFollowDistanceHorizontal || diffY > maxFollowDistanceVertical)
            transform.position = Vector2.Lerp(transform.position, toFollow.transform.position, Time.deltaTime * snapSpeed);
        else
            transform.position = Vector2.Lerp(transform.position, 
                toFollow.transform.position + (new Vector3(vel.x, vel.y, 0) / lookahead), 
                Time.deltaTime * followSpeed);
        SnapToConstraints();
    }

    private void SnapToConstraints()
    {
        transform.position = new Vector3(
            Mathf.Min(maxHorizontal, Mathf.Max(minHorizontal, transform.position.x)),
            Mathf.Min(maxVertical, Mathf.Max(minVertical, transform.position.y)),
            -10);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 0.5f, 0.25f);
        Gizmos.DrawCube(transform.position, new Vector3(2 * maxFollowDistanceHorizontal, 2 * maxFollowDistanceVertical));
    }
}

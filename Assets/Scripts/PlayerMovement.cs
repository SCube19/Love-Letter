using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Direction {Left = -1, None = 0, Right = 1};

    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float friction = 0.2f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float verticalClamp = 7f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundBoxExtra = 0.4f;

    private Direction direction = 0;

    private bool isJumping = false;
    private float lastGroundTime = 0f;
    private float lastJumpTime = 0f;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            direction = Direction.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction = Direction.Right;
        else 
            direction = Direction.None;
        SpriteFlip();
        SetAnimationParameters();
    }

    void FixedUpdate()
    {
        HandleMovement();
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
            Jump();
        ClampVertical();
    }

    public bool IsGrounded()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        RaycastHit2D raycast = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size - new Vector3(0.1f, 0, 0), 0f, Vector2.down, groundBoxExtra, groundLayer);
        return raycast.collider != null;
    }

    private void SpriteFlip()
    {
        if (direction == Direction.Left)
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction == Direction.Right)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void SetAnimationParameters()
    {
        GetComponent<Animator>().SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    private void HandleMovement()
    {
        float targetSpeed = (float)direction * maxSpeed;

        float speedDiff = targetSpeed - rb.velocity.x;

        float acc = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = speedDiff * acc;

        rb.AddForce(Vector2.right * movement);

        if (IsGrounded() && direction == Direction.None)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));
            rb.AddForce((-Mathf.Sign(rb.velocity.x) * amount) * Vector2.right, ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        isJumping = true;
        lastGroundTime = 0;
        lastJumpTime = 0;

    }

    private void ClampVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(verticalClamp)));
    }

    public void OnDrawGizmos()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireCube(bc.bounds.center - new Vector3(0, bc.bounds.extents.y, 0), new Vector3(bc.bounds.size.x - 0.1f, 2 * groundBoxExtra, 0.1f));
    }
}

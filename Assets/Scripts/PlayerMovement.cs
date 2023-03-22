using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1 };

    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float friction = 0.2f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float verticalClamp = 7f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundBoxExtra = 0.4f;

    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpCutMultiplier = 0.3f;
    [SerializeField] private float fallGravityScale = 3f;

    [SerializeField] private float dashForce = 1f;
    [SerializeField] private float dashCooldown = 1f;
    public float DashCooldown
    {
        get { return dashCooldown; }
    }

    //consider doing separate script
    [SerializeField] private GameObject impactDust;

    private Direction direction = 0;

    private bool isJumping = false;
    private float lastGroundTime = 0f;
    private float lastJumpTime = 0f;
    private bool holdsJump = false;

    private float initialGravityScale;

    //dash script
    private bool dash = false;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;

    public event Action OnDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        initialGravityScale = rb.gravityScale;

        //GameObject.Find("UI").transform.Find("DashTimer").GetComponent<TimedEvent>().SetWaitTime(dashCooldown);

        StartCoroutine(HandleDash());
    }

    void Update()
    {
        if (IsGrounded())
        {
            if (lastGroundTime > 0)
                PlayImpactDust();
            lastGroundTime = 0;
        }
        else
            lastGroundTime += Time.deltaTime;

        HandleActions();
        SpriteFlip();
        SetAnimationParameters();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        HandleFall();
        ClampVertical();
    }

    private void HandleActions()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
            lastJumpTime = 0;
        else
            lastJumpTime += Time.deltaTime;

        holdsJump = Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.LeftArrow))
            direction = Direction.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction = Direction.Right;
        else
            direction = Direction.None;

        if (Input.GetKeyDown(KeyCode.X))
            dash = true;
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
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("verticalSpeed", rb.velocity.y);
        anim.SetBool("isJumping", isJumping);
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

    private void HandleJump()
    {
        if (!isJumping && lastGroundTime < coyoteTime && lastJumpTime < jumpBufferTime)
            Jump();

        if (!holdsJump && isJumping && rb.velocity.y > 0)
            rb.AddForce(jumpCutMultiplier * rb.velocity.y * Vector2.down, ForceMode2D.Impulse);

        if (rb.velocity.y <= 0)
            isJumping = false;
    }

    private void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        isJumping = true;
        PlayImpactDust();
    }

    private void HandleFall()
    {
        rb.gravityScale = rb.velocity.y < 0 ? fallGravityScale : initialGravityScale;
    }

    private void ClampVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(verticalClamp)));
    }

    private void PlayImpactDust()
    {
        Instantiate(impactDust, transform.position - new Vector3(0, bc.bounds.extents.y, 1), Quaternion.identity);
    }

    private IEnumerator HandleDash()
    {
        while (true)
        {
            if (dash)
            {
                Vector3 dashImpulse = new(Mathf.Sign(rb.velocity.x) * dashForce, 0, 0);
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                rb.AddForce(dashImpulse, ForceMode2D.Impulse);
                anim.SetTrigger("dash");
                GetComponent<EchoEffect>().Play();
                GetComponent<Invincibility>().MakeInvincible();
                OnDash?.Invoke();
                yield return new WaitForSeconds(dashCooldown);
                dash = false;
            }
            yield return new WaitUntil(() => dash);
        }
    }

    public void EndDash()
    {
        Debug.Log("enddash");
        GetComponent<EchoEffect>().Stop();
    }

    public void OnDrawGizmos()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireCube(bc.bounds.center - new Vector3(0, bc.bounds.extents.y, 0), new Vector3(bc.bounds.size.x - 0.1f, 2 * groundBoxExtra, 0.1f));
    }
}
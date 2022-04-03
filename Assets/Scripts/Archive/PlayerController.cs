using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: Header("Movement")]
    [field: SerializeField] public float moveSpeed { get; set; } = 2.0f;
    [SerializeField] private float jumpForce = 250f;

    [Header("Ground")]
    [SerializeField] private Transform groundPoint; // feet
    [SerializeField] private float groundRadius = 0.2f; // radius of ground checker
    [SerializeField] private LayerMask groundLayers; // objects considered as ground

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //private AudioSource audSrc;

    private float horizontalInput;
    private bool facingRight = true;
    private bool isJump = true;

    void Start()
    {
        //Get references
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //audSrc = GetComponent<AudioSource>();
    }
    public void Wow()
    {
        Debug.Log("Wat");
    }

    void Update()
    {
        //Get the movement input from the user
        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));//for anim
        //Flip
        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = !facingRight;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        //move based on physics update
        float horizontalMovement = horizontalInput * moveSpeed;
        body.velocity = new Vector2(horizontalMovement, body.velocity.y);

        if (isJump && isGrounded())
        {
            //audSrc.volume = 0.25f;
            //audSrc.Play();
            AudioManager.Instance.PlaySound("Jump", 0.25f);
            body.AddForce(new Vector2(0, jumpForce));
            isJump = false;
        }
    }
    /// <summary>
    /// This will return true if our feet is touching the ground, false if not
    /// </summary>
    /// <returns>boolean</returns>
    private bool isGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, groundRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;
        }
        return false;
    }

    //Powerup
    public void IncreaseSpeed()
    {
        StartCoroutine(SpeedIncrease());
    }

    IEnumerator SpeedIncrease()
    {
        moveSpeed += 5;
        yield return new WaitForSeconds(3.0f);
        moveSpeed -= 5;
    }

}

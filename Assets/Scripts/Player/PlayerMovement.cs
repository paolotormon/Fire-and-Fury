using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [field: Header("Movement")]
    [field: SerializeField] public float speed { get; set; } = 7.0f;
    [field: SerializeField] public float jumpPower = 20.0f;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float horizontalInput;
    private bool justJumped;
    private float wallJumpCooldown; //for delays between wall jumps

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left/right
        if (horizontalInput > 0f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0f)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("grounded", isGrounded());
        //Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                justJumped = true;
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        //Set animation params
        //arrow keys not pressed, horizontal input == 0
        //arrow keys are pressed, horizontal input !=0
        anim.SetBool("run", horizontalInput != 0);

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if (justJumped)
            Jump(); justJumped = false;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            anim.SetTrigger("jump");
            AudioManager.Instance.PlaySound("Jump", 0.25f);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else if (onWall() && !isGrounded())
        {
            AudioManager.Instance.PlaySound("Jump", 0.25f);
            //flip off the wall
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //2 forces to push away from the wall, right and up or left and up
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        //origin, size, angle, direction, distance, contactfilter/layer
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    //Powerup
    public void IncreaseStats(float speedVal = 2.0f, float jumpVal = 5.0f)
    {
        this.speed += speedVal;
        this.jumpPower += jumpVal;
        //StartCoroutine(SpeedIncrease(speedVal, jumpVal));
    }

    //IEnumerator SpeedIncrease(float speedVal, float jumpVal)
    //{
    //    this.speed += speedVal;
    //    this.jumpPower += jumpVal;
    //    yield return new WaitForSeconds(3.0f);
    //    this.speed -= speedVal;
    //}
}

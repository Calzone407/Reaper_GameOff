using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [Header ("Horizontal Movement Variables:")]
    [SerializeField] private float speed;
    public float moveInput;
    bool left;
    bool right;
    [Space]
    [Header ("Vertical Movement Variables:")]
    public LayerMask Ground;
    public Transform groundCheck;
    public float checkRadius;
    public float jumpForce;
    public float fallSpeed;
    private float pressedJump;
    public float pressedJumpTime;
    public float groundRememberTime;
    private float groundRemember;
    private bool isFacingRight;
    [Space]
    [Header ("Wall Sliding Variables")]
    private bool isTouchingFront;
    private bool wallSliding;

    public bool wallJumping;
    [Header("wall variables")]
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public float wallSlidingSpeed;
    public float wallClimbSpeed;
    public bool isWallClimbing;
    public Transform frontCheck;
    private float moveDirection;
    private float _recentDirection;
    private bool wasMovingLeft;
    private bool wasMovingRight;
    [Space]
    [Header ("Misc")]
    public _Scythe scythe;
    public GameObject player;
    public GameObject g_scythe;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
      rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
      //wall jump starts here

      if (wallJumping)
      {
          rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
      }
      
      Slide();
    }
    // Update is called once per frame
    void Update()
    {
      Run();
      Jump();
      if (Input.GetKeyDown(KeyCode.Space) && wallSliding)
      {
          wallJumping = true;
          Invoke(nameof(setWallJumpToFalse), wallJumpTime); //Get the Name
      }
    }

    void Run()
    {
      left = Input.GetKey(KeyCode.A);
      right = Input.GetKey(KeyCode.D);

      if(left && !right)
      {
        _recentDirection = -1;
        wasMovingLeft = true;
        wasMovingRight = false;
      }
      else if(!left && right)
      {
         _recentDirection = 1;
         wasMovingRight = true;
         wasMovingLeft = false;
      }
      else if(wasMovingLeft && right)
      {
        _recentDirection = 1;
      }
      else if(wasMovingRight && left)
      {
        _recentDirection = -1;
      }
      else
      {
        _recentDirection = 0;
      }
      if(_recentDirection == 1 && isFacingRight)
      {
        Flip();
      }
      else if(_recentDirection == -1 && !isFacingRight)
      {
        Flip();
      }
      if(left || right)
      {
        anim.SetBool("isRunning", true);
      }
      else
      {
        anim.SetBool("isRunning", false);
      }
      moveDirection = _recentDirection;
    }

    void Jump()
    {
      bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);

       if (isGrounded)
       {
           groundRemember = groundRememberTime;
       }

       pressedJump -= Time.deltaTime;
       if (Input.GetKeyDown(KeyCode.Space))
       {
           pressedJump = pressedJumpTime;
           anim.SetBool("isJumping", true);
       }
       else
       {
         anim.SetBool("isJumping", false);
       }

       if (wallSliding == false)
       {
           if ((groundRemember > 0) && (pressedJump > 0))
           {
               pressedJump = 0;
               groundRemember = 0;
               rb.velocity = new Vector2(rb.velocity.x, jumpForce);
           }

           if (Input.GetKeyUp(KeyCode.Space))
           {
               if (rb.velocity.y > 0)
               {
                   rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallSpeed);
               }
           }
       }
    }
    private void Slide()
{
    float moveInputs = Input.GetAxisRaw("Horizontal");
    moveInput = moveInputs;
    float moveInputY = Input.GetAxisRaw("Vertical");
    bool isGroundeds = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
    isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, Ground);


    if (isTouchingFront && Input.GetKey(KeyCode.C))
    {
        wallSliding = false;
        isWallClimbing = true;
        rb.velocity = new Vector2(0, 0);
        rb.velocity = new Vector2(rb.velocity.x, moveInputY * wallClimbSpeed);
    }
    else
    {
        isWallClimbing = false;
        rb.gravityScale = 1;

    }
    if (isTouchingFront && isGroundeds == false && moveInputs != 0/* && !isWallClimbing*/)
    {
        wallSliding = true;
    }
    else
    {
        wallSliding = false;
    }
    if (wallSliding)
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }





}
void setWallJumpToFalse()
{
    wallJumping = false;
}

void Flip()
{
  transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
  isFacingRight = !isFacingRight;
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Private
    private Animator playerAnimator;

    private bool isCurrentlyClimbing = false, isCurrentlyJumping = false, isCurrentlyMoving = false, isCurrentlySprinting = false;
    private bool isJumpBeingPressed = false;

    private float horizontalMovement = 0f, verticalMovement = 0f;
    private float originalGravityScale, originalLinearDrag, originalMass;
    private float timeToBuildUpSprint = 0.5f, timeToSwitchDirection = 0.1875f;

    private Rigidbody2D rb2D;
    #endregion

    #region Public
    public int jumpHeight = 16;
    public int climbSpeed = 4, movementSpeed = /*1*/ 8;

    public List<string> colorsAcquired = new List<string>();
    #endregion

    void Start() 
    { 
        playerAnimator = GetComponent<Animator>(); 
        rb2D = GetComponent<Rigidbody2D>();

        originalGravityScale = rb2D.gravityScale;
        originalLinearDrag = rb2D.drag;
        originalMass = rb2D.mass;
    }

    void Update() { getInput(); }

    void FixedUpdate() { movement(); }

    private void climb() 
    {
        if (!isCurrentlyClimbing)
        {
            if(rb2D.gravityScale != originalGravityScale && rb2D.mass != originalMass)
            {
                rb2D.gravityScale = originalGravityScale;
                rb2D.mass = originalMass;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0) 
            { 
                changeMovementState("move");

                rb2D.gravityScale = originalGravityScale;
                rb2D.mass = originalMass;
            }
            else { rb2D.gravityScale = 0f; rb2D.mass = 6f; }
        }
    }

    private void getInput() 
    {
        if (!Input.GetKeyDown(KeyCode.Space)) { isJumpBeingPressed = false; }
        else { isJumpBeingPressed = true; }

        movementState();
    }

    private void movement()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        climb();

        if (horizontalMovement != 0f) 
        { 
            if(horizontalMovement > 0f) { GetComponent<SpriteRenderer>().flipX = false; }
            else { GetComponent<SpriteRenderer>().flipX = true; }

            isCurrentlyMoving = true;

            playerAnimator.SetBool("isMoving", true);
        }
        else 
        { 
            isCurrentlyMoving = false;

            if(!isCurrentlyMoving && !isCurrentlySprinting) { playerAnimator.SetBool("isMoving", false); }

            if (rb2D.velocity.x == 0f) { if (timeToSwitchDirection != 0.1875f) { timeToSwitchDirection = 0.1875f; } }
        }

        // Jump (lines 64 - 70)
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, new Vector2(0.075f, 0.02f), 0f, Vector2.down, 1.25f, layerMask);
        
        if(hit2D.collider != null) { isCurrentlyJumping = false; }
        else { isCurrentlyJumping = true; }

        if (!isCurrentlyJumping && isJumpBeingPressed) { rb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse); }

        sprint();

        rb2D.AddForce(new Vector2(horizontalMovement * movementSpeed, verticalMovement), ForceMode2D.Impulse);

        if(rb2D.velocity.x > movementSpeed) { rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y); }
        else if(rb2D.velocity.x < -movementSpeed) { rb2D.velocity = new Vector2(-movementSpeed, rb2D.velocity.y); }
    }

    private void movementState()
    {
        if (!isCurrentlyClimbing) { verticalMovement = 0f; horizontalMovement = Input.GetAxisRaw("Horizontal"); }
        else { verticalMovement = Input.GetAxisRaw("Vertical") * climbSpeed; }
    }

    private void sprint()
    {
        if (!isCurrentlyMoving) 
        {
            if (!isCurrentlySprinting) { if (timeToBuildUpSprint != 0f) { timeToBuildUpSprint = 0f; } }
            else
            {
                if (timeToSwitchDirection > 0f) { timeToSwitchDirection -= Time.deltaTime; }
                else 
                {
                    isCurrentlySprinting = false;

                    movementSpeed /= 2;
                }
            }
        }
        else if(isCurrentlyMoving)
        {
            if(!isCurrentlySprinting)
            {
                if (timeToBuildUpSprint >= 0.5f)
                {
                    isCurrentlySprinting = true;

                    movementSpeed *= 2;

                    timeToBuildUpSprint = 0f;
                }
                else { timeToBuildUpSprint += Time.deltaTime; }
            }
        }
    }

    public void changeMovementState(string _movementState)
    {
        switch (_movementState)
        {
            case "climb": isCurrentlyClimbing = true; isCurrentlyMoving = false; break;
            case "move": isCurrentlyClimbing = false; isCurrentlyMoving = true; break;
        }
    }
}
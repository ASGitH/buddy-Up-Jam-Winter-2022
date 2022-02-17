using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Private
    private Animator playerAnimator;

    private bool isCurrentlyJumping = false, isCurrentlyMoving = false, isCurrentlySprinting = false;

    private float horizontalMovement = 0f;
    private float originalLinearDrag;
    private float timeToBuildUpSprint = 0.5f, timeToSwitchDirection = 0.1875f;

    private Rigidbody2D rb2D;
    #endregion

    #region Public
    public int jumpHeight = 16;
    public int playerSpeed = /*1*/ 8;
    #endregion

    void Start() { playerAnimator = GetComponent<Animator>(); rb2D = GetComponent<Rigidbody2D>(); originalLinearDrag = rb2D.drag; }

    void Update() { getInput(); }

    void FixedUpdate() { movement(); }

    void OnTriggerEnter2D(Collider2D collision) { if (collision.tag == "Ground") { Debug.Log("false"); isCurrentlyJumping = false; } }
    void OnTriggerExit2D(Collider2D collision) { if (collision.tag == "Ground") { Debug.Log("true"); isCurrentlyJumping = true; } }

    private void getInput() { horizontalMovement = Input.GetAxisRaw("Horizontal"); }

    private void movement()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

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

            if (rb2D.velocity.x == 0f) { if (timeToSwitchDirection != 0.1875f) { timeToSwitchDirection = 0.1875f; } }

            playerAnimator.SetBool("isMoving", false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isCurrentlyJumping) { rb2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse); }

        sprint();

        Vector2 movementVector = new Vector2(horizontalMovement, 0f);

        rb2D.AddForce(new Vector2(horizontalMovement * playerSpeed, 0f), ForceMode2D.Impulse);

        if(rb2D.velocity.x > playerSpeed) { rb2D.velocity = new Vector2(playerSpeed, rb2D.velocity.y); }
        else if(rb2D.velocity.x < -playerSpeed) { rb2D.velocity = new Vector2(-playerSpeed, rb2D.velocity.y); }
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

                    playerSpeed /= 2;
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

                    playerSpeed *= 2;

                    timeToBuildUpSprint = 0f;
                }
                else { timeToBuildUpSprint += Time.deltaTime; }
            }
        }
    }
}
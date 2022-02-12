using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Private
    private bool isCurrentlyMoving = false, isCurrentlySprinting = false;

    private float timeToBuildUpSprint = 3f, timeToSwitchDirection = 0.5f;
    #endregion

    #region Public
    public float playerSpeed = 1f;
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();   
    }

    private void movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); float verticalMovement = Input.GetAxis("Vertical");

        if(horizontalMovement != 0f || verticalMovement != 0f) { isCurrentlyMoving = true; }
        else if(horizontalMovement == 0f && verticalMovement == 0f) 
        { 
            if(isCurrentlySprinting)
            {
                isCurrentlySprinting = false;

                playerSpeed /= 2f;
            }
            if(timeToBuildUpSprint != 0f) { timeToBuildUpSprint = 0f; }

            isCurrentlyMoving = false;
        }

        sprint();

        Vector3 movementVector = new Vector3(horizontalMovement, verticalMovement);

        transform.position += Vector3.ClampMagnitude(movementVector, 1f) * playerSpeed * Time.deltaTime;
    }

    private void sprint()
    {
        if (!isCurrentlyMoving)
        {
            if (!isCurrentlySprinting) { timeToBuildUpSprint = 0f; }
            else
            {
                // If the player has released the movement key, give the player a short amount of time to change direction, if they do change direction, don't affect the speed,
                // if they don't after the given amount of time, then decrease the speed again
            }
        }
        else if(isCurrentlyMoving && !isCurrentlySprinting)
        {
            if (timeToBuildUpSprint >= 3f)
            {
                isCurrentlySprinting = true;

                playerSpeed *= 2f;
            }
            else { timeToBuildUpSprint += Time.deltaTime; }
        }
    }
}
